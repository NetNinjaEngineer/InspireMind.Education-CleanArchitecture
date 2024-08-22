using InspireMind.Education.MVC.Helpers;
using InspireMind.Education.MVC.Models;
using InspireMind.Education.MVC.Services.Base;

namespace InspireMind.Education.MVC.Contracts;

public interface IAuthService
{
    Task<Result<RegisterResult>> RegisterAsync(RegisterVM registerModel);
    Task<LoginResultVM> LoginAsync(LoginVM loginModel);
    Task<RequestConfirmEmailResult> RequestConfirmEmailAsync(RequestConfirmEmailVM requestConfirmEmailVM);
    Task<Result<string>> ForgetPassword(ForgetPasswordModel forgetPasswordModel);
    Task<Result<string>> ResetPassword(string email, string token, ResetPasswordModel resetPasswordModel);
    Task Logout();
}
