using Ecommerce.API.Exceptions.Common;
using FluentValidation;

namespace Ecommerce.API.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleFluentValidationExceptionAsync(context, ex);
            }
            catch (ApiException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(
                    context,
                    new InternalServerErrorException("An unexpected error occurred. " + ex.Message));
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ApiException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var response = new
            {
                message = exception.Message,
                statusCode = exception.StatusCode
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }

        private static Task HandleFluentValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            //context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errors = exception.Errors.Select(error => new
            {
                error.PropertyName,
                error.ErrorMessage
            });

            var response = new
            {
                message = "Validation failed",
                errors
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }
}
