namespace InspireMind.Education.MVC.Models;

public class ConfirmEmailVM
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}
