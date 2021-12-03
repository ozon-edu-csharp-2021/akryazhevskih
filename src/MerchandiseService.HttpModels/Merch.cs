using System;
using System.Collections.Generic;

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
        public long Id { get; set; }

        /// <summary>
        /// Тип мерча
        /// </summary>
        public MerchType Type { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public MerchStatus Status { get; set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// Дата создания запроса
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? IssuedAt { get; set; }

        /// <summary>
        /// Товары
        /// </summary>
        public IEnumerable<MerchItem>? Items { get; set; }
    }
}