using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.DTOs.Topic;
using MediatR;

namespace InspireMind.Education.Application.Features.Topics.Requests.Commands
{
    public sealed class CreateTopicCommand : IRequest<Result<TopicDto>>
    {
        public TopicForCreationDto Topic { get; set; }
    }
}
