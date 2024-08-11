using InspireMind.Education.Api.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController(IMediator mediator) : AppControllerBase(mediator)
{

}
