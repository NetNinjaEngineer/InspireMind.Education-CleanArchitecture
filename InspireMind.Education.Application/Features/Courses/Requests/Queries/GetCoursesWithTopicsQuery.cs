using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.DTOs.Course;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;
using MediatR;

namespace InspireMind.Education.Application.Features.Courses.Requests.Queries;

public sealed class GetCoursesWithTopicsQuery : IRequest<Result<Pagination<CourseDto>>>
{
    public CourseRequestParameters Parameters { get; set; }
}
