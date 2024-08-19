namespace InspireMind.Education.MVC.Models;

public class LoginResult
{
    public bool IsSucessfull { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }

}
