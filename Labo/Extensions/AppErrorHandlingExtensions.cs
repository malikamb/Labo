using Labo.API.DTO;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Text.Json;

namespace Labo.API.Extensions
{
    public static class AppErrorHandlingExtensions
    {
        public static void UseException(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next(context);
                }
                catch (ValidationException e)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 400;
                    await HandleExceptionAsync(context, e);
                }
                catch (AuthenticationException)
                {
                    context.Response.StatusCode = 400;
                    await HandleExceptionAsync(context, "Bad credentials");
                }
                catch (UnauthorizedAccessException)
                {
                    context.Response.StatusCode = 401;
                    await HandleExceptionAsync(context, "Unauthorized");
                }
                catch (KeyNotFoundException e)
                {
                    context.Response.StatusCode = 404;
                    await HandleExceptionAsync(context, e.Message);
                }
            });
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, ValidationException exception)
        {

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ValidationErrorDTO(exception)));
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, string message)
        {
            await httpContext.Response.WriteAsync(message);
        }
    }
}
