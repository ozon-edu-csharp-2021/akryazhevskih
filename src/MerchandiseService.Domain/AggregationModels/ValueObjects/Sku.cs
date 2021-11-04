using System;
using System.Collections.Generic;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    /// <summary>
    /// Артикул товара
    /// </summary>
    public class Sku : ValueObject
    {
        public Sku(
            long code,
            string name
        )
        {
            if (code < 1)
            {
                throw new SkuException("Code cannot be less than 1");
            }
            
            if (String.IsNullOrEmpty(name))
            {
                throw new SkuException("Name cannot be empty");
            }
            
            Code = code;
            Name = name;
        }

        /// <summary>
        /// Код товара
        /// </summary>
        public long Code { get; }
        
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
            yield return Name;
        }
    }
}