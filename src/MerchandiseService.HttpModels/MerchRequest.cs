using System;

namespace MerchandiseService.HttpModels
{
    /// <summary>
    /// Запрос на выдачу мерча 
    /// </summary>
    public class MerchRequest
    {
        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        public Guid Id { get; set; }
        
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