using InspireMind.Education.Application.DTOs.Common;

namespace InspireMind.Education.Application.DTOs.Course;
public record CourseDto : BaseDto
{
    public string? CourseName { get; set; }
    public int Duration { get; set; }
    public Guid? TopicId { get; set; }
    public string? TopicName { get; set; }
}
