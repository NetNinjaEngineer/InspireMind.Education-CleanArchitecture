using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Emails.Requests.Commands;
using InspireMind.Education.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmailController(IMediator mediator) : AppControllerBase(mediator)
{
    [HttpPost]
    public async Task<ActionResult<Result<bool>>> SendEmail(Email email)
        => CustomResult(await _mediator.Send(new SendEmailCommand { EmailRequest = email }));
}
