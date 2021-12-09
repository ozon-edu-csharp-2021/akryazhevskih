using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Contracts;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CreateMerchCommandHandler(
            IMerchRepository merchRepository,
            IMerchPackRepository merchPackRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            ILogger<CreateMerchCommandHandler> logger)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Cannot be null");
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository), "Cannot be null");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "Cannot be null");
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "Cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }

        public async Task<Merch> Handle(CreateMerchCommand command, CancellationToken cancellationToken)
        {
            if (!Size.TryParse((int)command.Size, out var size))
            {
                throw new SizeException($"Unsupported size {command.Size}");
            }

            if (!MerchType.TryParse((int)command.MerchType, out var merchType))
            {
                throw new MerchTypeException($"Unsupported merch type {command.MerchType}");
            }

            await _unitOfWork.StartTransactionAsync(cancellationToken);

            var existingMerch = await _merchRepository.GetAsync(command.EmployeeEmail, merchType, cancellationToken);
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
                    _logger.LogError(ex, "Error when check merch pack expansion");
                }

                throw new MerchAlreadyExistException($"Merch with type equal {merchType.Name} for employee {command.EmployeeEmail} already exist");
            }

            var merchPack = await _merchPackRepository.GetAsync(merchType, size, cancellationToken);
            if (merchPack is null)
            {
                throw new MerchPackNullException($"Merch pack for type {merchType.Name} and size {size.Name} not found");
            }

            var merchPackItems = merchPack.GetItems();

            var employee = new Employee
            {
                Person = new Person(command.EmployeeName),
                Email = new Email(command.EmployeeEmail)
            };

            var manager = new Manager
            {
                Person = new Person(command.ManagerName),
                Email = new Email(command.ManagerEmail)
            };

            var merch = Merch.Create(employee, manager, merchType, size);

            merch = await _merchRepository.CreateAsync(merch, cancellationToken);

            foreach (var item in merchPackItems)
            {
                var merchItem = MerchItem.Create(merch.Id, item.Sku, item.Quantity, item.Size);

                if (!merch.TryAddMerchItem(merchItem, out var reason))
                {
                    _logger.LogWarning($"Failed to add item: {reason}");
                }

                await _merchRepository.CreateAsync(merchItem, cancellationToken);
            }

            merch.SetStatusInWork();

            await _merchRepository.UpdateAsync(merch, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return merch;
        }
    }
}