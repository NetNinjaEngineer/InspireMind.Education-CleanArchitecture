using InspireMind.Education.MVC.Models;

namespace InspireMind.Education.MVC.Contracts;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterVM registerModel);
    Task<LoginResultVM> LoginAsync(LoginVM loginModel);
    Task<RequestConfirmEmailResult> RequestConfirmEmailAsync(RequestConfirmEmailVM requestConfirmEmailVM);
}
