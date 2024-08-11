using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Authentication.Handlers.Result;
using InspireMind.Education.Application.Models.Identity;

namespace InspireMind.Education.Application.Contracts.Identity;

public interface IAuthService
{
    Task<Result<LoginResult>> Login(LoginModel request);
    Task<Result<RegisterResult>> Register(RegisterModel request);
    Task<Result<string>> ForgetPassword(ForgetPasswordModel forgetModel);
    Task<Result<string>> ResetPassword(string email, string token, ResetPasswordModel resetModel);
}
