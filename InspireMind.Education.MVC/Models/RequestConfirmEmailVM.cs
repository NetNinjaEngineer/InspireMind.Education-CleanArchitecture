using System.ComponentModel.DataAnnotations;

namespace InspireMind.Education.MVC.Models;

public class RequestConfirmEmailVM
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Not valid email address format")]
    public string Email { get; set; } = null!;

    [Required]
    public string ClientUri { get; set; } = null!;
}
