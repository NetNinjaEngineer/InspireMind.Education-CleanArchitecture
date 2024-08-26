namespace InspireMind.Education.Application.Features.Courses.DTOs;
public record CourseDto
{
    public Guid Id { get; set; }
    public string? CourseName { get; set; }
    public int Duration { get; set; }
    public Guid? TopicId { get; set; }
    public string? TopicName { get; set; }
}
