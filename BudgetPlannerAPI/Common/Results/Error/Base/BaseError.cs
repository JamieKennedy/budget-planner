using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Results.Error.Base
{
    record DetailExtensions(Guid ErrorId, IEnumerable<string> Reasons);

    public class BaseError : FluentResults.Error
    {
        public static int StatusCode => StatusCodes.Status500InternalServerError;
        public Guid ErrorId { get; init; }

        public BaseError(string message) : base(message)
        {
            ErrorId = Guid.NewGuid();
        }
        public ProblemDetails ToProblemDetails(string instance, string title)
        {
            var details = new ProblemDetails
            {
                Status = StatusCode,
                Title = title,
                Detail = Message,
                Instance = instance
            };

            var extensions = BuildExtensions();

            details.Extensions["ErrorId"] = extensions.ErrorId;
            details.Extensions["Reasons"] = extensions.Reasons;



            return details;
        }

        private DetailExtensions BuildExtensions()
        {
            return new DetailExtensions(ErrorId, Reasons.Select(reason => reason.Message));
        }


    }
}
