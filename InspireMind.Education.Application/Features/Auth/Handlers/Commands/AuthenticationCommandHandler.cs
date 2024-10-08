﻿using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Features.Auth.Handlers.Result;
using InspireMind.Education.Application.Features.Auth.Requests.Commands;
using MediatR;

namespace InspireMind.Education.Application.Features.Auth.Handlers.Commands;
public sealed class AuthenticationCommandHandler(IAuthService authService)
    : IRequestHandler<RegisterCommand, Result<RegisterResult>>,
        IRequestHandler<LoginCommand, Result<LoginResult>>,
        IRequestHandler<ForgetPasswordCommand, Result<string>>,
        IRequestHandler<ResetPasswordCommand, Result<string>>,
        IRequestHandler<RequestConfirmEmailCommand, Result<string>>,
        IRequestHandler<ConfirmEmailCommand, Result<string>>
{
    public async Task<Result<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        => await authService.Register(request.RegisterModel);

    public async Task<Result<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        => await authService.Login(request.LoginModel);

    public async Task<Result<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        => await authService.ForgetPassword(request.ForgetRequest);

    public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        => await authService.ResetPassword(request.Email, request.Token, request.ResetRequest);

    public async Task<Result<string>> Handle(RequestConfirmEmailCommand request, CancellationToken cancellationToken)
        => await authService.RequestConfirmEmail(request.RequestConfirmModel);

    public async Task<Result<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        => await authService.ConfirmEmail(request.Email, request.Token);
}
