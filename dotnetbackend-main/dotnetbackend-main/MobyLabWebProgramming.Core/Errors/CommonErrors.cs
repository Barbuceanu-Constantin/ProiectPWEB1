using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage JobNotFound => new(HttpStatusCode.NotFound, "Job doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage JobFailAdd => new(HttpStatusCode.NotAcceptable, "Job failed to be added!", ErrorCodes.CannotAdd);
    public static ErrorMessage RaionFailAdd => new(HttpStatusCode.NotAcceptable, "Raion failed to be added!", ErrorCodes.CannotAdd);
    public static ErrorMessage RaionFailGet => new(HttpStatusCode.NotAcceptable, "Raion failed to be get!", ErrorCodes.Unknown);

    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}
