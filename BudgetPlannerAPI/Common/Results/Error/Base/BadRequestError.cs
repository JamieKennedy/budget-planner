using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Results.Error.Base
{
    public class BadRequestError : BaseError
    {
        public static new int StatusCode => StatusCodes.Status400BadRequest;
        public BadRequestError(string message) : base(message) { }

        public ProblemDetails ToProblemDetails(string instance)
        {
            return ToProblemDetails(instance, "There was a problem with the request");
        }
    }
}
