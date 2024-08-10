using Microsoft.Extensions.Localization;
using System.Net;

namespace InspireMind.Education.Application.Bases;

public class BaseResponseHandler
{
    public readonly IStringLocalizer<BaseResponseHandler> _localizer;

    public BaseResponseHandler(IStringLocalizer<BaseResponseHandler> localizer)
    {
        _localizer = localizer;
    }

    public Result<T> Deleted<T>()
    {
        return new Result<T>()
        {
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = _localizer["DeletedSuccessfully"]
        };
    }

    public Result<T> Success<T>(T entity, object meta = null)
    {
        return new Result<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = _localizer["Successfully"],
            Meta = meta
        };
    }

    public Result<T> Unauthorized<T>()
    {
        return new Result<T>()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Succeeded = true,
            Message = _localizer["UnAuthorized"]
        };
    }

    public Result<T> BadRequest<T>(string message, List<string> errors = null)
    {
        return new Result<T>()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? _localizer["BadRequest"] : message,
            Errors = errors
        };
    }

    public Result<T> Conflict<T>(string message = null)
    {
        return new Result<T>()
        {
            StatusCode = HttpStatusCode.Conflict,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? _localizer["Conflict"] : message
        };
    }

    public Result<T> UnprocessableEntity<T>(string message = null)
    {
        return new Result<T>()
        {
            StatusCode = HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? _localizer["UnprocessableEntity"] : message
        };
    }

    public Result<T> NotFound<T>(string message = null)
    {
        return new Result<T>()
        {
            StatusCode = HttpStatusCode.NotFound,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? _localizer["NotFound"] : message
        };
    }

    public Result<T> Created<T>(T entity, object meta = null)
    {
        return new Result<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.Created,
            Succeeded = true,
            Message = _localizer["Created"],
            Meta = meta
        };
    }
}
