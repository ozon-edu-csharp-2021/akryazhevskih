using MediatR;
using MerchandiseService.HttpModels;
using Merch = MerchandiseService.Domain.AggregationModels.MerchAggregate.Merch;

namespace MerchandiseService.Infrastructure.Commands.CreateMerch
{
    public class CreateMerchCommand : IRequest<Merch>
    {
        /// <summary>
        /// Тип набора
        /// </summary>
        public MerchType MerchType { get; init; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long EmployeeId { get; init; }

        /// <summary>
        /// Размер
        /// </summary>
        public Size Size { get; init; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; init; }
    }
}