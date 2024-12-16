using System.Net;

namespace AirSense.Domain.Exceptions;

public class BadRequestException(string message, params string[] errorDetails) : ExceptionBase(message, errorDetails)
{
    public override HttpStatusCode ResponseStatusCode => HttpStatusCode.BadRequest;
}