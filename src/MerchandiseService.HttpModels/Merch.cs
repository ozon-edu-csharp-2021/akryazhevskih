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
        /// Размер мерча
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Email сотрудника
        /// </summary>
        public string EmployeeEmail { get; set; }

        /// <summary>
        /// ФИО менеджера
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// Email менеджера
        /// </summary>
        public string ManagerEmail { get; set; }

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