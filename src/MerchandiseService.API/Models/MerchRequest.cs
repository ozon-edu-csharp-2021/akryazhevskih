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
        /// Идентификатор сотрудника
        /// </summary>
        [Required]
        [Range(1, long.MaxValue)]
        public long? EmployeeId { get; set; }

        /// <summary>
        /// Размер
        /// </summary>
        [Required]
        public Size Size { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}