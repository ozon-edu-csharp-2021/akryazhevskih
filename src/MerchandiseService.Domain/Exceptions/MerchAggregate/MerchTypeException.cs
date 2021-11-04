using System;

namespace MerchandiseService.Domain.Exceptions.MerchAggregate
{
    public class MerchTypeException : Exception
    {
        public MerchTypeException(string message)
            : base(message)
        {
        }

        public MerchTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}