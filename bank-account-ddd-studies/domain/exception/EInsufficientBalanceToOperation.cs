using System;
using System.Runtime.Serialization;

namespace bank_account_ddd_studies.domain.exception
{
    [Serializable]
    public class EInsufficientBalanceToOperationException : Exception
    {
        public EInsufficientBalanceToOperationException() : base("Balance is insufficient to operation")
        { }

        public EInsufficientBalanceToOperationException(string message) : base(message)
        { }

        public EInsufficientBalanceToOperationException(string message, Exception innerException) : base(message, innerException)
        { }

        protected EInsufficientBalanceToOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
