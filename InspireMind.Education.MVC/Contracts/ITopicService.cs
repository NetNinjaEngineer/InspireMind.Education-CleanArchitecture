using InspireMind.Education.MVC.Services.Base;

namespace InspireMind.Education.MVC.Contracts;

public interface ITopicService
{
    Task<IReadOnlyList<TopicDto>?> GetTopics();
}
