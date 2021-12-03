using System;

namespace MerchandiseService.Infrastructure.Repositories.Infrastructure.Exceptions
{
    public class TransactionNullException : Exception
    {
        public TransactionNullException(string message)
            : base(message)
        {
        }

        public TransactionNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
