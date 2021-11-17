using System;

namespace MerchandiseService.Domain.Exceptions.MerchAggregate
{
    public class MerchAlreadyExistException : Exception
    {
        public MerchAlreadyExistException(string message)
            : base(message)
        {
        }

        public MerchAlreadyExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}