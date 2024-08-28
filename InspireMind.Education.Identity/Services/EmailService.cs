using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Models.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace InspireMind.Education.Identity.Services;
public class EmailService(
    IStringLocalizer<BaseResponseHandler> localizer,
    IOptions<SendGridSettings> sendGridSettings)
    : BaseResponseHandler(localizer), IEmailsService
{
    private readonly SendGridSettings _sendGridSettings = sendGridSettings.Value;

    public async Task<Result<bool>> SendEmail(Email emailMessage)
    {
        var client = new SendGridClient(_sendGridSettings.ApiKey);

        var message = MailHelper.CreateSingleEmail(
            from: new EmailAddress(_sendGridSettings.FromEmail, _sendGridSettings.FromName),
            to: new EmailAddress(emailMessage.To),
            subject: emailMessage.Subject,
            plainTextContent: null,
            htmlContent: emailMessage.Body
            );

        var response = await client.SendEmailAsync(message, CancellationToken.None);
        return Success(response.StatusCode == HttpStatusCode.OK);
    }
}
