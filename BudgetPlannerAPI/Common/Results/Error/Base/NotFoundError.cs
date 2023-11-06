using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Results.Error.Base
{
    public class NotFoundError : BaseError
    {
        public NotFoundError(string message) : base(message) { }

        public static new int StatusCode => StatusCodes.Status404NotFound;

        public ProblemDetails ToProblemDetails(string instance)
        {
            return ToProblemDetails(instance, "The requested resource could not be found");
        }

    }
}
