using System;

namespace MerchandiseService.Domain.Exceptions.MerchPackAggregate
{
    public class MerchPackException : Exception
    {
        public MerchPackException(string message)
            : base(message)
        {
        }

        public MerchPackException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}