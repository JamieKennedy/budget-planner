using Common.Exceptions;
using Common.Models.Error;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Diagnostics;

using Newtonsoft.Json;

namespace API.Middleware;

public static class ExceptionHandler
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder builder, ILoggerManager logger)
    {
        _ = builder.UseExceptionHandler(app =>
        {
            app.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature is not null)
                {
                    // get the error from the exception
                    var exception = contextFeature.Error;

                    var statusCode = GetStatusCodeFromException(exception);
                    var message = exception.Message;

                    var errorDto = new ErrorDto(statusCode, message);

                    // log the exception
                    logger.LogError(errorDto.ToString());

                    context.Response.StatusCode = statusCode;

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));
                }
            });
        });
    }

    private static int GetStatusCodeFromException(Exception exception)
    {
        return exception switch
        {
            IException ie => ie.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}