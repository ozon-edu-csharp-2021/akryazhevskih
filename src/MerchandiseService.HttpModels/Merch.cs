using System;

namespace MerchandiseService.HttpModels
{
    /// <summary>
    /// Мерч
    /// </summary>
    public class Merch
    {
        /// <summary>
        /// Уникальный идентификатор мерча
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