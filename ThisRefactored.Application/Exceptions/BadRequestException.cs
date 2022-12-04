using System.Net;
using System.Runtime.Serialization;

namespace ThisRefactored.Application.Exceptions;

/// <summary>
///     Use this exception when doing delayed property checks outside of validators.
/// </summary>
[Serializable]
public class BadRequestException : StatusCodeException
{
    public BadRequestException() : base((int)HttpStatusCode.BadRequest)
    {
    }

    protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public BadRequestException(string? message) : base(message, (int)HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string? message, Exception? innerException) : base(message,
                                                                                      innerException,
                                                                                      (int)HttpStatusCode.BadRequest)
    {
    }
}