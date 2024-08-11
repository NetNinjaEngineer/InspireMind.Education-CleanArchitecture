using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Authentication.Handlers.Result;
using InspireMind.Education.Application.Features.Authentication.Requests.Commands;
using InspireMind.Education.Application.Models.Identity;

namespace InspireMind.Education.Application.Contracts.Identity;

public interface IAuthService
{
    Task<Result<LoginResult>> Login(LoginCommand request);
    Task<Result<RegisterResult>> Register(RegisterCommand request);
    Task<Result<string>> ForgetPassword(ForgetPasswordModel forgetModel);
    Task<Result<string>> ResetPassword(ResetPasswordModel resetModel);
}
