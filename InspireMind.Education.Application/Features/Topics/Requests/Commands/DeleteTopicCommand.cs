using MediatR;

namespace InspireMind.Education.Application.Features.Topics.Requests.Commands
{
    public sealed class DeleteTopicCommand(Guid topicId) : IRequest<Unit>
    {
        public Guid TopicId { get; } = topicId;
    }
}
