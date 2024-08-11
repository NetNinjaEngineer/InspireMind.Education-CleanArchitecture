using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Models.Identity;

namespace InspireMind.Education.Application.Contracts.Identity
{
    public interface IEmailsService
    {
        public Task<Result<bool>> SendEmail(Email emailMessage);
    }
}
