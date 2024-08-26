using InspireMind.Education.Application.Features.Courses.DTOs;

namespace InspireMind.Education.Application.Features.Topics.DTOs;
public record TopicWithRelatedCoursesDto
{
    public Guid Id { get; set; }
    public string? TopicName { get; set; }
    public IEnumerable<CourseForListDto> Courses { get; set; } = [];
}
