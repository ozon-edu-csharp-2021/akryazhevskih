using System;

namespace MerchandiseService.Domain.Exceptions.EmployeeAggregate
{
    public class EmployeeException : Exception
    {
        public EmployeeException(string message)
            : base(message)
        {
        }

        public EmployeeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}