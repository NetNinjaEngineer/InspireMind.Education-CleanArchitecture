using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Models.Identity;
using MediatR;

namespace InspireMind.Education.Application.Features.Authentication.Requests.Commands;
public class RequestConfirmEmailCommand : IRequest<Result<string>>
{
    public RequestConfirmEmailModel RequestConfirmModel { get; set; } = null!;
}
