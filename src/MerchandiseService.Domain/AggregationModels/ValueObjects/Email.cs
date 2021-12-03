using System.Collections.Generic;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    /// <summary>
    /// Email
    /// </summary>
    public class Email : ValueObject
    {
        public Email(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new EmailException("Value cannot be empty");
            }

            Value = email;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}