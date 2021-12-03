using System.ComponentModel.DataAnnotations;
using MerchandiseService.HttpModels;

namespace MerchandiseService.API.Models
{
    /// <summary>
    /// Модель запроса на выдачу мерча
    /// </summary>
    public class MerchRequest
    {
        /// <summary>
        /// Тип мерча
        /// </summary>
        [Required]
        public MerchType Type { get; set; }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [Required]
        public string EmployeeName { get; init; }

        /// <summary>
        /// Email сотрудника
        /// </summary>
        [Required]
        [EmailAddress]
        public string EmployeeEmail { get; init; }

        /// <summary>
        /// ФИО менеджера
        /// </summary>
        [Required]
        public string ManagerName { get; init; }

        /// <summary>
        /// Email менеджера
        /// </summary>
        [Required]
        [EmailAddress]
        public string ManagerEmail { get; init; }

        /// <summary>
        /// Размер
        /// </summary>
        [Required]
        public Size Size { get; set; }
    }
}