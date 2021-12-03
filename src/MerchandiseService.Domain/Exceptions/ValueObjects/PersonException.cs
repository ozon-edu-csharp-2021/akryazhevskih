using System;

namespace MerchandiseService.Domain.Exceptions.ValueObjects
{
    public class PersonException : Exception
    {
        public PersonException(string message)
            : base(message)
        {
        }

        public PersonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}