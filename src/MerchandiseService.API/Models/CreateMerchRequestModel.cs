﻿using MerchandiseService.HttpModels;

namespace MerchandiseService.API.Models
{
    public class CreateMerchRequestModel
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