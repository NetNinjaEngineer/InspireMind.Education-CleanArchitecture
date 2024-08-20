using InspireMind.Education.MVC.Services.Base;

namespace InspireMind.Education.MVC.Contracts;

public interface ICoursesService
{
    Task<CourseDtoPagination?> GetPaginatedCourses(
        int? PageNumber,
        int? PageSize,
        Guid? TopicId,
        string? SearchTerm,
        CourseOrderingOptions? OrderingOptions);



}
