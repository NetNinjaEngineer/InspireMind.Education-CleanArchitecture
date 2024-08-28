namespace InspireMind.Education.Application.Features.Auth.Handlers.Result;
public class LoginResult(
    bool isSucessfull,
    string token,
    string userId,
    string email,
    string username)
{
    public bool IsSucessfull { get; } = isSucessfull;
    public string Token { get; } = token;
    public string UserId { get; } = userId;
    public string Email { get; } = email;
    public string Username { get; } = username;
}
