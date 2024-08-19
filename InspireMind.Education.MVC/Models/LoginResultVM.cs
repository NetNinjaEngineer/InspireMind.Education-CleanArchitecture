namespace InspireMind.Education.MVC.Models;

public class LoginResultVM
{
    public LoginResultVM(bool isSuccessfull, bool isEmailConfirmed)
    {
        IsSuccessfull = isSuccessfull;
        IsEmailConfirmed = isEmailConfirmed;
    }

    public bool IsSuccessfull { get; set; }
    public bool IsEmailConfirmed { get; set; }
}