using InspireMind.Education.Application.DTOs.Common;
using InspireMind.Education.Application.DTOs.Course;

namespace InspireMind.Education.Application.DTOs.Topic;
public record TopicWithRelatedCoursesDto : BaseDto
{
    public string? TopicName { get; set; }
    public IEnumerable<CourseForListDto> Courses { get; set; } = [];
}
