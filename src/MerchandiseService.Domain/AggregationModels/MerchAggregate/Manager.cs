using System.Collections.Generic;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    /// <summary>
    /// Менеджер, ответственный за выдачу
    /// </summary>
    public class Manager : ValueObject
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public Email Email { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Person;
            yield return Email;
        }
    }
}
