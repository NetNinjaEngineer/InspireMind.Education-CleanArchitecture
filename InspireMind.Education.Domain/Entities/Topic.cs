using InspireMind.Education.Domain.Entities.Common;

namespace InspireMind.Education.Domain.Entities;

public class Topic : BaseEntity
{
    public string? TopicName { get; set; }
    public ICollection<Course> Courses { get; set; } = [];
}
