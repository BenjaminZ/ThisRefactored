using System.Net;
using System.Runtime.Serialization;

namespace ThisRefactored.Application.Exceptions;

[Serializable]
public class NotFoundException : StatusCodeException
{
    public NotFoundException() : base((int)HttpStatusCode.NotFound)
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotFoundException(string? message) : base(message, (int)HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message,
                                                                                innerException,
                                                                                (int)HttpStatusCode.NotFound)
    {
    }
}