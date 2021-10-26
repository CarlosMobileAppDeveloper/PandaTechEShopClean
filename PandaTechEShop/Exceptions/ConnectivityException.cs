using System;
using System.Runtime.Serialization;

namespace PandaTechEShop.Exceptions
{
    public class ConnectivityException : Exception
    {
        public ConnectivityException()
        {
        }

        public ConnectivityException(string message)
            : base(message)
        {
        }

        public ConnectivityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ConnectivityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
