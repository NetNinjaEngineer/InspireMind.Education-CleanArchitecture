using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Domain.Entities;

namespace InspireMind.Education.Application.Specifications;
public class GetAllTopicsWithCoursesSpecification : BaseSpecification<Topic>
{
    public GetAllTopicsWithCoursesSpecification()
    {
        Includes.Add(x => x.Courses);
    }

    public GetAllTopicsWithCoursesSpecification(TopicRequestParams topicRequestParams) : base
        (topic =>
            string.IsNullOrEmpty(topicRequestParams.SearchTerm) || topic.TopicName!.ToLower().Contains(topicRequestParams.SearchTerm)
        )
    {
        Includes.Add(x => x.Courses);

        if (topicRequestParams.OrderingOptions is not null)
        {
            switch (topicRequestParams.OrderingOptions)
            {
                case TopicOrderingOptions.NameAsc:
                    AddOrderBy(x => x.TopicName!);
                    break;

                case TopicOrderingOptions.NameDesc:
                    AddOrderByDescending(x => x.TopicName!);
                    break;

                default:
                    AddOrderBy(x => x.TopicName!);
                    break;
            }
        }

        ApplyPagination((topicRequestParams.PageNumber - 1) * topicRequestParams.PageSize, topicRequestParams.PageSize);
    }
}
