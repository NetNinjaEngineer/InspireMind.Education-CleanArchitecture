using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Authentication.Handlers.Result;
using InspireMind.Education.Application.Features.Authentication.Requests.Commands;
using InspireMind.Education.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers;
[Route("api/account")]
[ApiController]
public class AccountController : AppControllerBase
{
    public AccountController(IMediator mediator) : base(mediator) { }


    [HttpPost("register")]
    public async Task<ActionResult<RegisterResult>> Register(RegisterModel request)
    {
        Result<RegisterResult> result = await _mediator.Send(new RegisterCommand { RegisterModel = request });

        return CustomResult(result);
    }


    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login(LoginModel request)
    {
        Result<LoginResult> result = await _mediator.Send(new LoginCommand { LoginModel = request });

        return Ok(result.Data);
    }

    [HttpPost("forget-password")]
    public async Task<ActionResult<string>> ForgetPassword(ForgetPasswordModel request)
    {
        Result<string> result = await _mediator.Send(new ForgetPasswordCommand { ForgetRequest = request });

        return CustomResult(result);
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<string>> ResetPassword(
        [FromQuery] string email,
        [FromQuery] string token,
        [FromBody] ResetPasswordModel request)
    {
        Result<string> result = await _mediator.Send(new ResetPasswordCommand
        {
            Email = email,
            Token = token,
            ResetRequest = request
        });

        return CustomResult(result);
    }


}
