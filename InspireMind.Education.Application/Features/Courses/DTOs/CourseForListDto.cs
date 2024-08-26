namespace InspireMind.Education.Application.Features.Courses.DTOs;
public record CourseForListDto
{
    public Guid Id { get; set; }
    public string? CourseName { get; set; }
    public int Duration { get; set; }
}
