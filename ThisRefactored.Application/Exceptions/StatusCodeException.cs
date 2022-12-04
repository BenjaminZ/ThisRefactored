using System.Runtime.Serialization;

namespace ThisRefactored.Application.Exceptions;

[Serializable]
public class StatusCodeException : Exception
{
    public StatusCodeException(int statusCode)
    {
        StatusCode = statusCode;
    }

    protected StatusCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public StatusCodeException(string? message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public StatusCodeException(string? message,
                               Exception? innerException,
                               int statusCode) : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}