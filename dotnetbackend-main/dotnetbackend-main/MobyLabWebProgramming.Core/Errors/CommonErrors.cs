using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage JobFailGet => new(HttpStatusCode.NotFound, "Job failed to be get!", ErrorCodes.EntityNotFound);
    public static ErrorMessage RaionFailGet => new(HttpStatusCode.NotFound, "Raion failed to be get!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProviderFailGet => new(HttpStatusCode.NotFound, "Provider failed to be get!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProductFailGet => new(HttpStatusCode.NotFound, "Product failed to be get!", ErrorCodes.EntityNotFound);

    public static ErrorMessage UserFailAdd => new(HttpStatusCode.Conflict, "User failed to be added!", ErrorCodes.CannotAdd);
    public static ErrorMessage JobFailAdd => new(HttpStatusCode.Conflict, "Job failed to be added!", ErrorCodes.CannotAdd);
    public static ErrorMessage RaionFailAdd => new(HttpStatusCode.Conflict, "Raion failed to be added!", ErrorCodes.CannotAdd);
    public static ErrorMessage ProviderFailAdd => new(HttpStatusCode.Conflict, "Provider failed to be added!", ErrorCodes.CannotAdd);
    public static ErrorMessage ProductFailAdd => new(HttpStatusCode.Conflict, "Product failed to be added!", ErrorCodes.CannotAdd);

    public static ErrorMessage UserFailUpdate => new(HttpStatusCode.BadRequest, "User failed to be updated!", ErrorCodes.CannotUpdate);
    public static ErrorMessage JobFailUpdate => new(HttpStatusCode.BadRequest, "Job failed to be updated!", ErrorCodes.CannotUpdate);
    public static ErrorMessage RaionFailUpdate => new(HttpStatusCode.BadRequest, "Raion failed to be updated!", ErrorCodes.CannotUpdate);
    public static ErrorMessage ProviderFailUpdate => new(HttpStatusCode.BadRequest, "Provider failed to be updated!", ErrorCodes.CannotUpdate);
    public static ErrorMessage ProductFailUpdate => new(HttpStatusCode.BadRequest, "Product failed to be updated!", ErrorCodes.CannotUpdate);

    public static ErrorMessage UserFailDelete => new(HttpStatusCode.NotFound, "User failed to be deleted!", ErrorCodes.CannotDelete);
    public static ErrorMessage JobFailDelete => new(HttpStatusCode.NotFound, "Job failed to be deleted!", ErrorCodes.CannotDelete);
    public static ErrorMessage RaionFailDelete => new(HttpStatusCode.NotFound, "Raion failed to be deleted!", ErrorCodes.CannotDelete);
    public static ErrorMessage ProviderFailDelete => new(HttpStatusCode.NotFound, "Provider failed to be deleted!", ErrorCodes.CannotDelete);
    public static ErrorMessage ProductFailDelete => new(HttpStatusCode.NotFound, "Product failed to be deleted!", ErrorCodes.CannotDelete);

    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}
