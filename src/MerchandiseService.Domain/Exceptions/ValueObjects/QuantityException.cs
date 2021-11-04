using System;

namespace MerchandiseService.Domain.Exceptions.ValueObjects
{
    public class QuantityException : Exception
    {
        public QuantityException(string message)
            : base(message)
        {
        }

        public QuantityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}