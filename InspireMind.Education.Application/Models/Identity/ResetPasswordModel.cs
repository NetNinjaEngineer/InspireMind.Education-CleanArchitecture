namespace InspireMind.Education.Application.Models.Identity;
public class ResetPasswordModel
{
    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}
