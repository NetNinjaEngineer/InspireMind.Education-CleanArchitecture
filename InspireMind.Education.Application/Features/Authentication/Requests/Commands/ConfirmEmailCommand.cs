using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Authentication.Requests.Commands;
public class ConfirmEmailCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}
