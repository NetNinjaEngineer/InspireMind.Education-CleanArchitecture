namespace InspireMind.Education.Application.Features.Authentication.Handlers.Result;
public class RegisterResult(bool isAuthSuccessfull, List<string>? errors = null)
{
    public bool IsAuthSuccessfull { get; } = isAuthSuccessfull;
    public List<string>? Errors { get; } = errors;
}
