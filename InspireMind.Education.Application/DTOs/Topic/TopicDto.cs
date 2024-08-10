using InspireMind.Education.Application.DTOs.Common;

namespace InspireMind.Education.Application.DTOs.Topic;

public sealed record TopicDto : BaseDto
{
    public string? TopicName { get; set; }
}
