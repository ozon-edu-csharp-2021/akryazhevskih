using MediatR;

namespace MerchandiseService.Infrastructure.Commands.CheckMerchPackExpansion
{
    public class CheckMerchPackExpansionCommand : IRequest<bool>
    {
        public CheckMerchPackExpansionCommand(long merchId)
        {
            MerchId = merchId;
        }

        /// <summary>
        /// Идентификатор мерча
        /// </summary>
        public long MerchId { get; }
    }
}