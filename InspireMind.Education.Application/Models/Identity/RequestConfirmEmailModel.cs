using System.ComponentModel.DataAnnotations;

namespace InspireMind.Education.Application.Models.Identity;
public class RequestConfirmEmailModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}
