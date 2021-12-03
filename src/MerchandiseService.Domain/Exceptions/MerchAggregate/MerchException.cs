using System;

namespace MerchandiseService.Domain.Exceptions.MerchAggregate
{
    public class MerchException : Exception
    {
        public MerchException(string message)
            : base(message)
        {
        }

        public MerchException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}