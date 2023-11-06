using System.Text.Json;

using Common.Results.Error.Base;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


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

                    var error = (BaseError) new BaseError(exception.Message).CausedBy(exception);



                    // log the exception
                    logger.LogError(exception);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var problemDetails = error.ToProblemDetails(context.Request.Path, "An Error has occurred");

                    await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, typeof(ProblemDetails)));
                }
            });
        });
    }
}