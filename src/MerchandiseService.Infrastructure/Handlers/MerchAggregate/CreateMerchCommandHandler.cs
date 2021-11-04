using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using MerchandiseService.Infrastructure.Commands.CreateMerch;
using MerchandiseService.Infrastructure.GrpcClients.StockApi;
using Microsoft.Extensions.Logging;
using Merch = MerchandiseService.Domain.AggregationModels.MerchAggregate.Merch;
using MerchType = MerchandiseService.Domain.AggregationModels.MerchAggregate.MerchType;
using Size = MerchandiseService.Domain.AggregationModels.ValueObjects.Size;

namespace MerchandiseService.Infrastructure.Handlers.MerchAggregate
{
    public class CreateMerchCommandHandler : IRequestHandler<CreateMerchCommand, Merch>
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IMerchItemRepository _merchItemRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IStockApiClient _stockApiClient;

        private readonly ILogger _logger;

        public CreateMerchCommandHandler(
            IMerchRepository merchRepository,
            IMerchItemRepository merchItemRepository,
            IMerchPackRepository merchPackRepository,
            IStockApiClient stockApiClient,
            ILogger<CreateMerchCommandHandler> logger)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Сan't be null");
            _merchItemRepository = merchItemRepository ?? throw new ArgumentNullException(nameof(merchItemRepository), "Сan't be null");
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository), "Сan't be null");
            _stockApiClient = stockApiClient ?? throw new ArgumentNullException(nameof(stockApiClient), "Сan't be null");
            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Сan't be null");
        }

        public async Task<Merch> Handle(CreateMerchCommand command, CancellationToken cancellationToken)
        {
            if (!Size.TryParse((int) command.Size, out var size))
            {
                throw new SizeException($"Unsupported size {command.Size}");
            }

            if (!MerchType.TryParse((int) command.MerchType, out var merchType))
            {
                throw new MerchTypeException($"Unsupported merch type {command.MerchType}");
            }

            var existingMerch = await _merchRepository.GetAsync(new Identifier(command.EmployeeId), merchType, cancellationToken);
            if (existingMerch is not null)
            {
                throw new MerchAlreadyExistException(
                    $"Merch with type equal {merchType.Name} for employee {command.EmployeeId} already exist");
            }

            var merchPack = await _merchPackRepository.GetAsync(merchType, size, cancellationToken);
            if (merchPack?.Items == null || !merchPack.Items.Any())
            {
                throw new MerchPackNullException(
                    $"Merch pack for type {merchType.Name} and size {size.Name} not found");
            }
            
            var employee = new Employee(new Identifier(command.EmployeeId), size, new Email(command.Email));

            var merch = await _merchRepository.CreateAsync(new Merch(employee, merchType), cancellationToken);
            
            foreach (var item in merchPack.Items)
            {
                var available = await _stockApiClient.GetStockItem(item.Sku.Code, cancellationToken);
                if (available == null)
                {
                    _logger.LogWarning($"Could not find position by sku {item.Sku.Code}({item.Sku.Name})");
                    continue;
                }

                if (available.Quantity == 0)
                {
                    continue;
                }
                
                var quantity = available.Quantity >= item.Quantity.Value
                    ? item.Quantity.Value
                    : available.Quantity;

                var isSuccessIssue = await _stockApiClient.IssueStockItem(new StockApiMerchItem
                {
                    Sku = item.Sku.Code,
                    Quantity = quantity
                }, cancellationToken);

                if (isSuccessIssue)
                {
                    var merchItem = new MerchItem(merch.Id, item.Sku, item.Quantity, new Quantity(quantity));

                    if (merch.TryAddMerchItem(merchItem))
                    {
                        await _merchItemRepository.CreateAsync(merchItem,cancellationToken);
                    }
                }
            }

            return merch;
        }
    }
}