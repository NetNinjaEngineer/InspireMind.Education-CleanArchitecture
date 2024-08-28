using FluentValidation;
using InspireMind.Education.Application.Exceptions;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Text.Json;

namespace InspireMind.Education.Api.Middleware;

internal class GlobalErrorHandlingMiddleware(RequestDelegate next,
                                           ILogger<GlobalErrorHandlingMiddleware> logger,
                                           IStringLocalizer<GlobalErrorHandlingMiddleware> localizer)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, context);
        }
    }

    private async Task HandleExceptionAsync(Exception ex, HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        ErrorResponse errorResponse;
        switch (ex)
        {
            case ValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                var errors = validationException.Errors.Select(x => x.ErrorMessage);
                errorResponse = new ErrorResponse(
                    statusCode: (int)HttpStatusCode.UnprocessableEntity,
                    errors: errors,
                    errorDetails: new ErrorDetails(
                        exceptionType: ex.GetType().Name,
                        stackTrace: ex.StackTrace,
                        source: ex.Source
                    ),
                    description: localizer["validationErrors"],
                    timeStamp: DateTime.UtcNow
                );
                break;

            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse = new ErrorResponse(
                    statusCode: (int)HttpStatusCode.NotFound,
                    errors: [ex.Message],
                    errorDetails: new ErrorDetails(
                        exceptionType: ex.GetType().Name,
                        stackTrace: ex.StackTrace,
                        source: ex.Source
                    ),
                    description: localizer["resourceNotFound"],
                    timeStamp: DateTime.UtcNow
                );
                break;

            default:
                errorResponse = new ErrorResponse(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    errors: [ex.Message],
                    errorDetails: new ErrorDetails(
                        exceptionType: ex.GetType().Name,
                        stackTrace: ex.StackTrace,
                        source: ex.Source
                    ),
                    description: localizer["ServerError"],
                    timeStamp: DateTime.UtcNow
                ); break;
        }

        await context.Response.WriteAsync(errorResponse.ToString());
    }

    private sealed class ErrorDetails(
        string? exceptionType,
        string? stackTrace,
        string? source)
    {
        public string? ExceptionType { get; set; } = exceptionType;
        public string? StackTrace { get; set; } = stackTrace;
        public string? Source { get; set; } = source;
    }

    private sealed class ErrorResponse(
        int statusCode,
        IEnumerable<string> errors,
        ErrorDetails errorDetails,
        string description,
        DateTime timeStamp)
    {
        public int StatusCode { get; set; } = statusCode;
        public IEnumerable<string> Errors { get; set; } = errors;
        public ErrorDetails ErrorDetails { get; set; } = errorDetails;
        public string Description { get; set; } = description;
        public DateTime TimeStamp { get; set; } = timeStamp;

        public override string ToString() =>
            JsonSerializer.Serialize(this,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }

}
