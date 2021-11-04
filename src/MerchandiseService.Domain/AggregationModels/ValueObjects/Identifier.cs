using System.Collections.Generic;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public class Identifier : ValueObject
    {
        public Identifier(long id)
        {
            if (id < 1)
            {
                throw new IdentifierException("ID cannot be less than 1");
            }
            
            Value = id;
        }
        
        public long Value { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}