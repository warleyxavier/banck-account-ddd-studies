using System;
using System.Runtime.Serialization;

namespace bank_account_ddd_studies.infra.exception
{
    [Serializable]
    public class EHandlerAlreadyRegisteredException : Exception
    {
        public EHandlerAlreadyRegisteredException() : base("Handler already registered in publisher")
        { }

        public EHandlerAlreadyRegisteredException(string message) : base(message)
        { }

        public EHandlerAlreadyRegisteredException(string message, Exception innerException) : base(message, innerException)
        { }

        protected EHandlerAlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
