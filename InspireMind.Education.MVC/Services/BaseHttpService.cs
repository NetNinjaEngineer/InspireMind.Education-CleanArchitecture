namespace InspireMind.Education.MVC.Services;

public abstract class BaseHttpService
{
    private readonly IHttpContextAccessor _context;

    protected BaseHttpService(IHttpContextAccessor context)
    {
        _context = context;
    }

    protected void AddBearerToken(string token)
    {

    }

}
