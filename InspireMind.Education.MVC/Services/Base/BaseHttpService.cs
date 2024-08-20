namespace InspireMind.Education.MVC.Services.Base;

public abstract class BaseHttpService(
    IHttpContextAccessor contextAccessor)
{
    protected string? GetJwtToken()
    {
        string? tokenValue = default;
        contextAccessor.HttpContext?.Request.Cookies.TryGetValue("token", out tokenValue);
        return tokenValue;
    }
}
