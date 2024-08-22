namespace InspireMind.Education.MVC.Models;

public class RegisterResult
{
    public bool IsAuthSuccessfull { get; set; }
    public List<string> Errors { get; set; } = [];
}
