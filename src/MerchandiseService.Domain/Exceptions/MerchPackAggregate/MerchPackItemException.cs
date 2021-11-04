using System;

namespace MerchandiseService.Domain.Exceptions.MerchPackAggregate
{
    public class MerchPackItemException : Exception
    {
        public MerchPackItemException(string message)
            : base(message)
        {
        }

        public MerchPackItemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}