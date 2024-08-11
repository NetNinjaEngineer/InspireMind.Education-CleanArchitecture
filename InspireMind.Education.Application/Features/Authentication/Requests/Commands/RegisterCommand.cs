using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Authentication.Handlers.Result;
using InspireMind.Education.Application.Models.Identity;
using MediatR;

namespace InspireMind.Education.Application.Features.Authentication.Requests.Commands;
public sealed class RegisterCommand : IRequest<Result<RegisterResult>>
{
    public RegisterModel RegisterModel { get; set; }
}
