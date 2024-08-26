namespace InspireMind.Education.Application.Features.Topics.DTOs;

public sealed record TopicDto
{
    public Guid Id { get; set; }
    public string? TopicName { get; set; }
}
