using System;

namespace MerchandiseService.Domain.Exceptions.MerchAggregate
{
    public class MerchNullException : Exception
    {
        public MerchNullException(string message)
            : base(message)
        {
        }
        
        public MerchNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}