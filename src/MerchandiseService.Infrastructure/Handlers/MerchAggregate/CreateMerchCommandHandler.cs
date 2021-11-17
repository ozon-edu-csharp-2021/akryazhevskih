using System;
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
using MerchandiseService.Infrastructure.Commands.CheckMerchPackExpansion;
using MerchandiseService.Infrastructure.Commands.CreateMerch;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Handlers.MerchAggregate
{
    public class CreateMerchCommandHandler : IRequestHandler<CreateMerchCommand, Merch>
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CreateMerchCommandHandler(
            IMerchRepository merchRepository,
            IMerchPackRepository merchPackRepository,
            IMediator mediator,
            ILogger<CreateMerchCommandHandler> logger)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Cannot be null");
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository), "Cannot be null");
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "Cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
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

            var existingMerch = await _merchRepository.GetAsync(command.EmployeeId, merchType, cancellationToken);
            if (existingMerch is not null)
            {
                var checkMerchPackExpansionCommand = new CheckMerchPackExpansionCommand(existingMerch.Id);
                
                try
                {
                    var checkResult = await _mediator.Send(checkMerchPackExpansionCommand, cancellationToken);
                    if (checkResult)
                    {
                        existingMerch.SetStatusInWork();
                        await _merchRepository.UpdateAsync(existingMerch, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when check merch pack expansion: {ex.Message}");
                }
                
                throw new MerchAlreadyExistException($"Merch with type equal {merchType.Name} for employee {command.EmployeeId} already exist");
            }

            var merchPack = await _merchPackRepository.GetAsync(merchType, size, cancellationToken);
            if (merchPack == null)
            {
                throw new MerchPackNullException($"Merch pack for type {merchType.Name} and size {size.Name} not found");
            }
            
            var merchPackItems = merchPack.GetMerchPackItems();
            
            var employee = new Employee(command.EmployeeId, size, new Email(command.Email));
            var merch = new Merch(employee, merchType);

            foreach (var item in merchPackItems)
            {
                var merchItem = new MerchItem(item.Sku, item.Quantity, item.Size);
                
                if (!merch.TryAddMerchItem(merchItem, out var reason))
                {
                    _logger.LogWarning($"Failed to add item: {reason}");
                }
            }

            merch.SetStatusInWork();
            
            await _merchRepository.CreateAsync(merch, cancellationToken);
            
            return merch;
        }
    }
}