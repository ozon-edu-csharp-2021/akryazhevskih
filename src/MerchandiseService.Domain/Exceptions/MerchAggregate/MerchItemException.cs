using System;

namespace MerchandiseService.Domain.Exceptions.MerchAggregate
{
    public class MerchItemException : Exception
    {
        public MerchItemException(string message)
            : base(message)
        {
        }
        
        public MerchItemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}