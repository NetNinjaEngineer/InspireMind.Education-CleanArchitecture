using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Features.Emails.Requests.Commands;
using MediatR;

namespace InspireMind.Education.Application.Features.Emails.Handlers.Commands;
public class EmailCommandHandler(IEmailsService emailsService)
    : IRequestHandler<SendEmailCommand, Result<bool>>
{

    public async Task<Result<bool>> Handle(SendEmailCommand request,
                                           CancellationToken cancellationToken)
        => await emailsService.SendEmail(request.EmailRequest);
}
