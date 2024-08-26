using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Models.Identity;
using MediatR;

namespace InspireMind.Education.Application.Features.Emails.Requests.Commands;
public class SendEmailCommand : IRequest<Result<bool>>
{
    public Email EmailRequest { get; set; } = null!;
}
