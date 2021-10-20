using System;
using System.Net;

namespace NerdStore.WebApp.MVC.Exceptions
{
    public class CustomHttpResponseException : Exception
    {
        public readonly HttpStatusCode StatusCode;

        public CustomHttpResponseException() { }

        public CustomHttpResponseException(string message, Exception innerException) : base (message, innerException) { }

        public CustomHttpResponseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
