namespace InspireMind.Education.Application.Features.Authentication.Requests.Commands;
public sealed class LoginCommand
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
