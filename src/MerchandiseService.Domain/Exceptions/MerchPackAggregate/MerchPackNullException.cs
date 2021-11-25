using System;

namespace MerchandiseService.Domain.Exceptions.MerchPackAggregate
{
    public class MerchPackNullException : Exception
    {
        public MerchPackNullException(string message)
            : base(message)
        {
        }

        public MerchPackNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}