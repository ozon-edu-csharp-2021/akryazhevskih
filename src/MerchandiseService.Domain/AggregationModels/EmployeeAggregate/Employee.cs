using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.EmployeeAggregate;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : Entity
    {
        public Employee(
            long id,
            Size size,
            Email email)
        {
            if (id <= 0)
            {
                throw new EmployeeException("ID cannot be less than 1");
            }
            
            if (size is null)
            {
                throw new EmployeeException("Size cannot be null");
            }
            
            if (email is null)
            {
                throw new EmployeeException("Email cannot be null");
            }
            
            Size = size;
            Email = email;
            SetId(id);
        }
        
        /// <summary>
        /// Размер
        /// </summary>
        public Size Size { get; }
        
        /// <summary>
        /// Email
        /// </summary>
        public Email Email { get; }

        protected void SetId(long id)
        {
            Id = id;
        }
    }
}