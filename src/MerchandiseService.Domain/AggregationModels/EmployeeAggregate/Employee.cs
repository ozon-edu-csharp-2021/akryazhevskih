using System.Collections.Generic;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.EmployeeAggregate;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : ValueObject
    {
        public Employee(
            Identifier id,
            Size size,
            Email email
        )
        {
            if (id is null)
            {
                throw new EmployeeException("ID cannot be null");
            }
            
            if (size is null)
            {
                throw new EmployeeException("Size cannot be null");
            }
            
            if (email is null)
            {
                throw new EmployeeException("Email cannot be null");
            }
            
            Id = id;
            Size = size;
            Email = email;
        }
        
        /// <summary>
        /// ID
        /// </summary>
        public Identifier Id { get; private set; } 
        
        /// <summary>
        /// Размер
        /// </summary>
        public Size Size { get; }
        
        /// <summary>
        /// Email
        /// </summary>
        public Email Email { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Size;
            yield return Email;
        }
    }
}