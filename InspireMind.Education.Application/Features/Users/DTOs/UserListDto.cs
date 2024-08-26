namespace InspireMind.Education.Application.Features.Users.DTOs;
public class UserListDto
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
