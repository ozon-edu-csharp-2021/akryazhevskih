using System;

namespace MerchandiseService.Domain.Exceptions.MerchAggregate
{
    public class MerchItemNullException : Exception
    {
        public MerchItemNullException(string message)
            : base(message)
        {
        }
        
        public MerchItemNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}