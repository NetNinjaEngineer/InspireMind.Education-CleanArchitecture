using InspireMind.Education.Api.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmailController(IMediator mediator) : AppControllerBase(mediator)
{

}
