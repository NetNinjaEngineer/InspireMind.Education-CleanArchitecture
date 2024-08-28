using InspireMind.Education.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InspireMind.Education.Api.Base;
public class AppControllerBase(IMediator mediator) : ControllerBase
{
    protected readonly IMediator _mediator = mediator;

    #region Actions

    public ObjectResult CustomResult<T>(Result<T> response)
    {
        return GetObjectResult(response);
    }

    // Overload for handling List responses
    public ObjectResult CustomResult<T>(Result<List<T>> response)
    {
        return GetObjectResult(response);
    }

    //public ObjectResult CustomResult<T>(PaginatedResult<T> response)
    //{
    //    return GetObjectResult(response);
    //}

    // Overload for handling List responses
    //public ObjectResult CustomResult<T>(PaginatedResult<List<T>> response)
    //{
    //    return GetObjectResult(response);
    //}
    //private ObjectResult GetObjectResult<T>(PaginatedResult<T> response)
    //{
    //    switch (response.StatusCode)
    //    {
    //        case HttpStatusCode.OK:
    //            return new OkObjectResult(response);
    //        case HttpStatusCode.Unauthorized:
    //            return new UnauthorizedObjectResult(response);
    //        case HttpStatusCode.BadRequest:
    //            return new BadRequestObjectResult(response);
    //        case HttpStatusCode.NotFound:
    //            return new NotFoundObjectResult(response);
    //        case HttpStatusCode.Accepted:
    //            return new AcceptedResult(string.Empty, response);
    //        case HttpStatusCode.UnprocessableEntity:
    //            return new UnprocessableEntityObjectResult(response);
    //        default:
    //            return new BadRequestObjectResult(response);
    //    }
    //}
    private ObjectResult GetObjectResult<T>(Result<T> response)
    {
        return response.StatusCode switch
        {
            HttpStatusCode.OK => new OkObjectResult(response),
            HttpStatusCode.Created => new CreatedResult(string.Empty, response),
            HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
            HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
            HttpStatusCode.NotFound => new NotFoundObjectResult(response),
            HttpStatusCode.Accepted => new AcceptedResult(string.Empty, response),
            HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(response),
            _ => new BadRequestObjectResult(response),
        };
    }

    #endregion
}
