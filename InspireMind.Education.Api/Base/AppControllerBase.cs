using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Base;
public class AppControllerBase : ControllerBase
{
    protected readonly IMediator _mediator;

    public AppControllerBase(IMediator mediator) => _mediator = mediator;
}
