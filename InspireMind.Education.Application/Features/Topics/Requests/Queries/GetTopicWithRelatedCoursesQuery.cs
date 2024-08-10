using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.DTOs.Topic;
using MediatR;

namespace InspireMind.Education.Application.Features.Topics.Requests.Queries;
public sealed class GetTopicWithRelatedCoursesQuery : IRequest<Result<TopicWithRelatedCoursesDto>>
{
    public Guid TopicId { get; set; }
}
