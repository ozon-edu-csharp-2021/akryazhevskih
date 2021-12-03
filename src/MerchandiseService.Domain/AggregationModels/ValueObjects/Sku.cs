using System.Collections.Generic;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    /// <summary>
    /// Товарная позиция
    /// </summary>
    public class Sku : ValueObject
    {
        public Sku(long code)
        {
            if (code < 1)
            {
                throw new SkuException("Code cannot be less than 1");
            }

            Code = code;
        }

        /// <summary>
        /// Код товара
        /// </summary>
        public long Code { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}