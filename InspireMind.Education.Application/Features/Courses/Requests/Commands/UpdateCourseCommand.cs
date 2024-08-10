using InspireMind.Education.Application.DTOs.Course;
using MediatR;

namespace InspireMind.Education.Application.Features.Courses.Requests.Commands;
public sealed class UpdateCourseCommand(Guid id, CourseForUpdateDto course) : IRequest<Unit>
{
    public Guid Id { get; } = id;
    public CourseForUpdateDto Course { get; } = course;
}
