using MerchandiseService.HttpModels;

namespace MerchandiseService.HttpClient.Models
{
    public class MerchRequest
    {
        /// <summary>
        /// Тип мерча
        /// </summary>
        public MerchType Type { get; set; }
        
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long EmployeeId { get; set; }
    }
}