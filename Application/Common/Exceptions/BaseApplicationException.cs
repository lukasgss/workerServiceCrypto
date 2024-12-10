using System.Net;

namespace Application.Common.Exceptions;

public abstract class BaseApplicationException : Exception
{
    public HttpStatusCode HttpStatusCode { get; private set; }

    protected BaseApplicationException(HttpStatusCode statusCode)
    {
        HttpStatusCode = statusCode;
    }

    protected BaseApplicationException(string message, HttpStatusCode statusCode) : base(message)
    {
        HttpStatusCode = statusCode;
    }

    protected BaseApplicationException(string message, Exception innerException, HttpStatusCode statusCode) : base(
        message, innerException)
    {
        HttpStatusCode = statusCode;
    }
}