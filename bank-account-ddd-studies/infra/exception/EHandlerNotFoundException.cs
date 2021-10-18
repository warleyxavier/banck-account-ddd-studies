using System;
using System.Runtime.Serialization;

namespace bank_account_ddd_studies.infra.exception
{
    [Serializable]
    public class EHandlerNotFoundException : Exception
    {
        public EHandlerNotFoundException() : base("Handler not found")
        { }

        public EHandlerNotFoundException(string message) : base(message)
        { }

        public EHandlerNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }

        protected EHandlerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
