using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Helpers;
using InspireMind.Education.MVC.Services.Base;

namespace InspireMind.Education.MVC.Services;

public class TopicService(
    IHttpContextAccessor contextAccessor,
    IHttpClientFactory httpClientFactory)
    : BaseHttpService(contextAccessor), ITopicService
{
    public async Task<IReadOnlyList<TopicDto>?> GetTopics()
    {
        var client = httpClientFactory.CreateClient(Constants.TopicsClient);
        // todo: handle expected errors

        var topics = await client.GetFromJsonAsync<IReadOnlyList<TopicDto>>(
            Constants.TopicsEndPointUri, CancellationToken.None);

        return topics;
    }
}
