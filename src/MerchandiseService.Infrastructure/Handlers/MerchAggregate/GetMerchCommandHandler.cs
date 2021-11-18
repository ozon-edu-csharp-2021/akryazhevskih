using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.Contracts;
using MerchandiseService.Infrastructure.Commands.CheckMerchPackExpansion;
using MerchandiseService.Infrastructure.Commands.GetMerch;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Handlers.MerchAggregate
{
    public class GetMerchCommandHandler : IRequestHandler<GetMerchCommand, Merch>
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public GetMerchCommandHandler(
            IMerchRepository merchRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            ILogger<GetMerchCommandHandler> logger)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Cannot be null");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "Cannot be null");
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "Cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }

        public async Task<Merch> Handle(GetMerchCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);

            var merch = await _merchRepository.GetAsync(command.MerchId, cancellationToken);
            if (merch is null)
            {
                return null;
            }
            
            var checkMerchPackExpansionCommand = new CheckMerchPackExpansionCommand(merch.Id);

            try
            {
                var checkResult = await _mediator.Send(checkMerchPackExpansionCommand, cancellationToken);
                if (checkResult)
                {
                    merch.SetStatusInWork();
                    await _merchRepository.UpdateAsync(merch, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when check merch pack expansion: {ex.Message}");
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return merch;
        }
    }
}