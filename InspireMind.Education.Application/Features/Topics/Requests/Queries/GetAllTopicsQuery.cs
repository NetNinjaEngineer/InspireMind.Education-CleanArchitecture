using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.DTOs.Topic;
using MediatR;

namespace InspireMind.Education.Application.Features.Topics.Requests.Queries
{
    public sealed class GetAllTopicsQuery : IRequest<Result<IReadOnlyList<TopicDto>>>;
}
