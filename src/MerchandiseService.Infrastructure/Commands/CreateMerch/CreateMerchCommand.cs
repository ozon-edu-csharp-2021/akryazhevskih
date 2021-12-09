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
        /// Размер
        /// </summary>
        public Size Size { get; init; }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public string EmployeeName { get; init; }

        /// <summary>
        /// Email сотрудника
        /// </summary>
        public string EmployeeEmail { get; init; }

        /// <summary>
        /// ФИО менеджера
        /// </summary>
        public string ManagerName { get; init; }

        /// <summary>
        /// Email менеджера
        /// </summary>
        public string ManagerEmail { get; init; }
    }
}