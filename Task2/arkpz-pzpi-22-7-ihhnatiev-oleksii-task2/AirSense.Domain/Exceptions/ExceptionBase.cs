using System.Net;

namespace AirSense.Domain.Exceptions;

public abstract class ExceptionBase(string message, params string[] errorDetails) : Exception(message)
{
    public abstract HttpStatusCode ResponseStatusCode { get; }

    public IReadOnlyList<string> ErrorDetails { get; } = errorDetails;
}