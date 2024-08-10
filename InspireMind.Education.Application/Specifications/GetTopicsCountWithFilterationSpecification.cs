using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Domain.Entities;

namespace InspireMind.Education.Application.Specifications;
public sealed class GetTopicsCountWithFilterationSpecification : BaseSpecification<Topic>
{
    public GetTopicsCountWithFilterationSpecification(TopicRequestParams topicRequestParams)
        : base(x =>
            string.IsNullOrEmpty(topicRequestParams.SearchTerm) || x.TopicName!.ToLower().Contains(topicRequestParams.SearchTerm)
        )
    {

    }
}