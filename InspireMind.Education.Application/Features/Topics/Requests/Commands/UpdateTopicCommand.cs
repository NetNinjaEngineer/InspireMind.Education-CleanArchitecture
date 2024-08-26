using InspireMind.Education.Application.Features.Topics.DTOs;
using MediatR;

namespace InspireMind.Education.Application.Features.Topics.Requests.Commands;

public sealed class UpdateTopicCommand(Guid topicId, TopicForUpdateDto updatedTopic) : IRequest<Unit>
{
    public Guid TopicId { get; } = topicId;
    public TopicForUpdateDto UpdatedTopic { get; } = updatedTopic;
}
