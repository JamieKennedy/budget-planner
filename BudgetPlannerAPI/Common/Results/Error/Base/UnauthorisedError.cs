using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Results.Error.Base
{
    public class UnauthorisedError : BaseError
    {
        public static new int StatusCode => StatusCodes.Status401Unauthorized;
        public UnauthorisedError(string message) : base(message) { }

        public ProblemDetails ToProblemDetails(string instance)
        {
            return ToProblemDetails(instance, "You are not authorised to access this data");
        }
    }
}
