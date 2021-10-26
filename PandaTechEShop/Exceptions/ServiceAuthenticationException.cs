using System;
namespace PandaTechEShop.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content, Uri requestUri)
        {
            Content = content;
            Uri = requestUri;
        }

        public string Content { get; }

        public Uri Uri { get; }
    }
}
