using System;

namespace MerchandiseService.Domain.Exceptions.ValueObjects
{
    public class SkuException : Exception
    {
        public SkuException(string message)
            : base(message)
        {
        }

        public SkuException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}