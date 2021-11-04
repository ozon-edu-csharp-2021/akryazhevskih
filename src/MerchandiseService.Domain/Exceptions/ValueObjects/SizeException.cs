using System;

namespace MerchandiseService.Domain.Exceptions.ValueObjects
{
    public class SizeException : Exception
    {
        public SizeException(string message)
            : base(message)
        {
        }

        public SizeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}