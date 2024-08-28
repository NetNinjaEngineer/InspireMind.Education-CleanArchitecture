namespace InspireMind.Education.Application.Features.Departments.DTOs;
public record DepartmentForCreateDto
{
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public DateOnly? ManageDate { get; set; }
}
