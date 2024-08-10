using FluentValidation;
using InspireMind.Education.Application.Exceptions;
using InspireMind.Education.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace InspireMind.Education.Application.Middleware;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
    private readonly IStringLocalizer<GlobalErrorHandlingMiddleware> _localizer;

    public GlobalErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<GlobalErrorHandlingMiddleware> logger,
        IStringLocalizer<GlobalErrorHandlingMiddleware> localizer)
    {
        _next = next;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
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
        ErrorDetails errorDetails = default!;
        switch (ex)
        {
            case ValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                var errors = validationException.Errors.Select(x => x.ErrorMessage);
                errorDetails = new((int)HttpStatusCode.UnprocessableEntity, errors, _localizer[SharedResourcesKeys.ValidationErrors]);
                break;

            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorDetails = new((int)HttpStatusCode.NotFound, [ex.Message], _localizer[SharedResourcesKeys.ResourceNotFound]);
                break;

            default:
                errorDetails = new((int)HttpStatusCode.InternalServerError, [ex.Message]);
                break;
        }

        await context.Response.WriteAsync(errorDetails.ToString());
    }

    internal sealed class ErrorDetails(int statusCode, IEnumerable<string> errors, string? description = null)
    {
        public int StatusCode { get; set; } = statusCode;
        public IEnumerable<string> Errors { get; set; } = errors;
        public string? Description { get; set; } = description;

        public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
