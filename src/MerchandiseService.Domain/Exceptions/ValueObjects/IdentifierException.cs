using System;

namespace MerchandiseService.Domain.Exceptions.ValueObjects
{
    public class IdentifierException : Exception
    {
        public IdentifierException(string message)
            : base(message)
        {
        }

        public IdentifierException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}