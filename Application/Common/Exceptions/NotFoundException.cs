using System.Net;

namespace Application.Common.Exceptions;

public sealed class NotFoundException : BaseApplicationException
{
    private const HttpStatusCode StatusCode = HttpStatusCode.NotFound;

    public NotFoundException() : base(StatusCode)
    {
    }

    public NotFoundException(string message) : base(message, StatusCode)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException, StatusCode)
    {
    }
}