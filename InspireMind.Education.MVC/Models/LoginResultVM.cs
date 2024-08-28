namespace InspireMind.Education.MVC.Models;

public class LoginResultVM(bool isSuccessfull, bool isEmailConfirmed)
{
    public bool IsSuccessfull { get; set; } = isSuccessfull;
    public bool IsEmailConfirmed { get; set; } = isEmailConfirmed;
}