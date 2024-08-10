using InspireMind.Education.Application.DTOs.Common;

namespace InspireMind.Education.Application.DTOs.Course;
public record CourseForListDto : BaseDto
{
    public string? CourseName { get; set; }
    public int Duration { get; set; }
}
