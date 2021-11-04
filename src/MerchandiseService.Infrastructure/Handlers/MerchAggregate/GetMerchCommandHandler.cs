using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Commands.GetMerch;

namespace MerchandiseService.Infrastructure.Handlers.MerchAggregate
{
    public class GetMerchCommandHandler : IRequestHandler<GetMerchCommand, Merch>
    {
        private readonly IMerchRepository _merchRepository;

        public GetMerchCommandHandler(
            IMerchRepository merchRepository)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Сan't be null");
        }

        public async Task<Merch> Handle(GetMerchCommand command, CancellationToken cancellationToken)
        {
            return await _merchRepository.GetAsync(new Identifier(command.MerchId), cancellationToken);
        }
    }
}