using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Domain.Entities;

namespace InspireMind.Education.Application.Specifications;
internal class GetCoursesWithTopicsSpecification : BaseSpecification<Course>
{
    public GetCoursesWithTopicsSpecification(CourseRequestParameters parameters) :
        base(c =>
            (string.IsNullOrEmpty(parameters.SearchTerm) ||
            c.CourseName!.ToLower()!.Contains(parameters.SearchTerm) ||
            c.Topic!.TopicName!.ToLower().Contains(parameters.SearchTerm))
            &&
            (string.IsNullOrEmpty(parameters.TopicId) || c.Topic.Id.ToString() == parameters.TopicId)
        )
    {
        Includes.Add(x => x.Topic!);

        if (parameters.OrderingOptions is not null)
        {
            switch (parameters.OrderingOptions)
            {
                case CourseOrderingOptions.DurationAsc:
                    AddOrderBy(c => c.Duration);
                    break;

                case CourseOrderingOptions.DurationDesc:
                    AddOrderByDescending(c => c.Duration);
                    break;

                case CourseOrderingOptions.NameAscDurationDesc:
                    AddOrderBy(c => c.CourseName!);
                    AddOrderByDescending(c => c.Duration);
                    break;

                case CourseOrderingOptions.NameDescDurationAsc:
                    AddOrderByDescending(c => c.CourseName!);
                    AddOrderBy(c => c.Duration);
                    break;


                default:
                    AddOrderBy(c => c.Duration);
                    break;
            }
        }

        ApplyPagination((parameters.PageNumber - 1) * parameters.PageSize, parameters.PageSize);
    }
}
