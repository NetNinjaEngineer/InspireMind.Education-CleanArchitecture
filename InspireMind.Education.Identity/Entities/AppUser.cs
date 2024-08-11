using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InspireMind.Education.Identity.Entities;
public class AppUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string? LastName { get; set; }
}
