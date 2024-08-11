﻿using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Features.Authentication.Handlers.Result;
using InspireMind.Education.Application.Features.Authentication.Requests.Commands;
using MediatR;

namespace InspireMind.Education.Application.Features.Authentication.Handlers.Commands;
public sealed class AuthenticationCommandHandler :
    IRequestHandler<RegisterCommand, Result<RegisterResult>>,
    IRequestHandler<LoginCommand, Result<LoginResult>>,
    IRequestHandler<ForgetPasswordCommand, Result<string>>,
    IRequestHandler<ResetPasswordCommand, Result<string>>
{
    private readonly IAuthService _authService;

    public AuthenticationCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        => await _authService.Register(request.RegisterModel);

    public async Task<Result<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        => await _authService.Login(request.LoginModel);

    public async Task<Result<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        => await _authService.ForgetPassword(request.ForgetRequest);

    public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        => await _authService.ResetPassword(request.Email, request.Token, request.ResetRequest);
}
