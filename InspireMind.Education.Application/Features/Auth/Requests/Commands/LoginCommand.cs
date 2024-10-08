﻿using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Auth.Handlers.Result;
using InspireMind.Education.Application.Models.Identity;
using MediatR;

namespace InspireMind.Education.Application.Features.Auth.Requests.Commands;
public sealed class LoginCommand : IRequest<Result<LoginResult>>
{
    public LoginModel LoginModel { get; set; }
}
