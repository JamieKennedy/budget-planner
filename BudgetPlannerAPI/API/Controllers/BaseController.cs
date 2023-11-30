using Common.DataTransferObjects.Authentication;
using Common.Results.Error.Base;
using Common.Utils;

using FluentResults;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IServiceManager serviceManager;
        protected readonly ILoggerManager loggerManager;


        private readonly IHttpContextAccessor _contextAccessor;

        public BaseController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor)
        {
            this.serviceManager = serviceManager;
            this.loggerManager = loggerManager;
            _contextAccessor = contextAccessor;
        }

        protected AuthIdentity AuthIdentity
        {
            get
            {
                return _contextAccessor?.HttpContext?.GetAuthIdentity() ?? new AuthIdentity();
            }
        }


        protected IActionResult HandleResult(FluentResults.Result result)
        {
            if (result.IsSuccess)
            {
                if (result.Successes.Any())
                {
                    // Should only be 1 success
                    var success = result.Successes.First();

                    // switch on the success type and create appropriate response, default to OK
                    return success switch
                    {
                        Common.Results.Success.Accepted => Accepted(),
                        _ => Ok()
                    };
                }

                // If no successes but result is success, return OK
                return Ok();
            }

            // Should only be 1 error
            var error = result.Errors.First();

            LogError(error, result, Request.Path);

            return MapError(error, Request?.Path ?? string.Empty);
        }

        protected IActionResult HandleResult<T>(FluentResults.Result<T> result)
        {
            if (result.IsSuccess)
            {
                if (result.Successes.Any())
                {
                    // Should only be 1 success
                    var success = result.Successes.First();

                    // switch on the success type and create appropriate response, default to OK
                    return success switch
                    {
                        Common.Results.Success.Created<T> ca => CreatedAtAction(ca.RouteName, ca.Values, ca.Item),
                        Common.Results.Success.Accepted<T> a => Accepted(a.Item),
                        _ => Ok(result.Value)
                    };
                }

                // If no successes but result is success, return OK
                return Ok(result.Value);
            }

            if (result.Errors.Count > 1)
            {

            }

            // Should only be 1 error
            var error = result.Errors.First();

            LogError(error, result.ToResult(), Request.Path);

            return MapError(error, Request.Path);

        }

        protected IActionResult MapError(IError error, string path = "")
        {
            // switch on the error type and return appropriate response, defaults to 500 response
            return error switch
            {
                NotFoundError nfe => NotFound(nfe.ToProblemDetails(path)),
                BadRequestError bre => BadRequest(bre.ToProblemDetails(path)),
                UnauthorisedError ue => Unauthorized(ue.ToProblemDetails(path)),
                BaseError be => StatusCode(StatusCodes.Status500InternalServerError, be.ToProblemDetails(path, "An unexpected error has occured")),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails() { Status = StatusCodes.Status500InternalServerError, Title = "An unexpected error has occured" })
            };
        }

        private void LogError(IError error, FluentResults.Result result, string? instance = null)
        {
            if (result.IsFailed)
            {
                if (error is BaseError be)
                {
                    if (instance is not null)
                    {
                        loggerManager.LogError("[{errorId}] {instance}  {message} ", be.ErrorId, instance, be.Message);
                    }
                    else
                    {
                        loggerManager.LogError("[{errorId}] {message} ", be.ErrorId, be.Message);
                    }
                }
                else
                {
                    // Otherwise just log with no context
                    result.LogIfFailed();
                }
            }


        }
    }
}
