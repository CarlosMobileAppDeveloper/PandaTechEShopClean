using System;
using System.Net.Http;

namespace PandaTechEShop.Exceptions
{
    public class HttpRequestExceptionEx : HttpRequestException
    {
        public HttpRequestExceptionEx(System.Net.HttpStatusCode code, Uri requestUri) : this(code, requestUri, null, null)
        {
        }

        public HttpRequestExceptionEx(System.Net.HttpStatusCode code, Uri requestUri, string message) : this(code, requestUri, message, null)
        {
        }

        public HttpRequestExceptionEx(System.Net.HttpStatusCode code, Uri requestUri, string message, Exception inner) : base(message, inner)
        {
            HttpCode = code;
            Uri = requestUri;
        }

        public System.Net.HttpStatusCode HttpCode { get; }
        public Uri Uri { get; }
    }
}
