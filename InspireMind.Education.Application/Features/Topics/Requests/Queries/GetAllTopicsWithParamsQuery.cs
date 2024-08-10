using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.DTOs.Topic;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;
using MediatR;

namespace InspireMind.Education.Application.Features.Topics.Requests.Queries;
public sealed class GetAllTopicsWithParamsQuery : IRequest<Result<Pagination<TopicDto>>>
{
    public TopicRequestParams TopicRequestParams { get; set; }
}
