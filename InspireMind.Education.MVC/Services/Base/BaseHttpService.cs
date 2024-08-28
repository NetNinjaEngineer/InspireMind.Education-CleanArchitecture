using InspireMind.Education.MVC.Contracts;

namespace InspireMind.Education.MVC.Services.Base;

public abstract class BaseHttpService(ILocalStorageService localStorageService)
{
    protected ILocalStorageService _localStorageService = localStorageService;

    protected string? GetJwtToken() => _localStorageService.GetStorageValue<string>("token");
}
