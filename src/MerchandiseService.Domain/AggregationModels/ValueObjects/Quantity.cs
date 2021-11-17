using System.Collections.Generic;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    /// <summary>
    /// Количество
    /// </summary>
    public class Quantity : ValueObject
    {
        public Quantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new QuantityException("Quantity cannot be less than 0");
            }
            
            Value = quantity;
        }
        
        public int Value { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}