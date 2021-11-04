using MediatR;

namespace MerchandiseService.Infrastructure.Commands.CheckMerchPackExpansion
{
    public class CheckMerchPackExpansionCommand : IRequest<bool>
    {
        /// <summary>
        /// Идентификатор мерча
        /// </summary>
        public long MerchId { get; init; }
    }
}