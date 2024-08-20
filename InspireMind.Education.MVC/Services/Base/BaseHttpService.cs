using InspireMind.Education.MVC.Contracts;

namespace InspireMind.Education.MVC.Services.Base;

public abstract class BaseHttpService
{
    protected ILocalStorageService _localStorageService;

    protected BaseHttpService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    protected string? GetJwtToken() => _localStorageService.GetStorageValue<string>("token");
}
