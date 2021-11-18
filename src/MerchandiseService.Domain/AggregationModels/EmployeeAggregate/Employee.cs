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

        public static Employee Create(long id, Size size, Email email)
        {
            return new Employee(id, size, email);
        }

        public void Update(Size size, Email email)
        {
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
        }

        /// <summary>
        /// Размер
        /// </summary>
        public Size Size { get; private set; }
        
        /// <summary>
        /// Email
        /// </summary>
        public Email Email { get; private set; }

        protected void SetId(long id)
        {
            Id = id;
        }
    }
}