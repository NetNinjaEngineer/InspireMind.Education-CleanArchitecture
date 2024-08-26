namespace InspireMind.Education.Application.Features.Auth.Handlers.Result;
public class LoginResult
{
    public bool IsSucessfull { get; }
    public string Token { get; }
    public string UserId { get; }
    public string Email { get; }
    public string Username { get; }

    public LoginResult(
        bool isSucessfull,
        string token,
        string userId,
        string email,
        string username)
    {
        IsSucessfull = isSucessfull;
        Token = token;
        UserId = userId;
        Email = email;
        Username = username;
    }


}
