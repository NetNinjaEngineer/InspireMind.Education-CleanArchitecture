using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Models.Identity;
using MediatR;

namespace InspireMind.Education.Application.Features.Auth.Requests.Commands;
public sealed class ResetPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public string Token { get; set; }
    public ResetPasswordModel ResetRequest { get; set; }
}
