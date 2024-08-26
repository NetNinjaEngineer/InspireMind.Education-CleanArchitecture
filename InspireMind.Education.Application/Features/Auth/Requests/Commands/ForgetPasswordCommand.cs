using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Models.Identity;
using MediatR;

namespace InspireMind.Education.Application.Features.Auth.Requests.Commands;
public sealed class ForgetPasswordCommand : IRequest<Result<string>>
{
    public ForgetPasswordModel ForgetRequest { get; set; }
}
