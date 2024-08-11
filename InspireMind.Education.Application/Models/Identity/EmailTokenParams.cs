using System.ComponentModel.DataAnnotations;

namespace InspireMind.Education.Application.Models.Identity;

public class EmailTokenParams
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;
}