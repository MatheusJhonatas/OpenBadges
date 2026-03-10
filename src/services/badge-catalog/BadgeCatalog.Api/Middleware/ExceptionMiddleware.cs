using BadgeCatalog.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BadgeCatalog.Api.Middleware;

public static class ExceptionMiddleware
{
    public static void UseGlobalExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerFeature>()?.Error;

                ProblemDetails problem;

                if (exception is ConcurrencyException concurrency)
                {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;

                    problem = new ProblemDetails
                    {
                        Title = "Concurrency conflict",
                        Detail = concurrency.Message,
                        Status = StatusCodes.Status409Conflict,
                        Type = "https://httpstatuses.com/409",
                        Instance = context.Request.Path
                    };
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    problem = new ProblemDetails
                    {
                        Title = "Unexpected error",
                        Detail = "An unexpected server error occurred.",
                        Status = StatusCodes.Status500InternalServerError,
                        Type = "https://httpstatuses.com/500",
                        Instance = context.Request.Path
                    };
                }

                await context.Response.WriteAsJsonAsync(problem);
            });
        });
    }
}