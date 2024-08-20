using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Helpers;
using InspireMind.Education.MVC.Services.Base;
using System.Net.Http.Headers;

namespace InspireMind.Education.MVC.Services;

public class TopicService(
    IHttpClientFactory httpClientFactory,
    ILocalStorageService localStorageService) : BaseHttpService(localStorageService), ITopicService
{
    public async Task<IReadOnlyList<TopicDto>?> GetTopics()
    {
        var client = httpClientFactory.CreateClient(Constants.TopicsClient);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetJwtToken());
        // todo: handle expected errors

        var topics = await client.GetFromJsonAsync<IReadOnlyList<TopicDto>>(
            Constants.TopicsEndPointUri, CancellationToken.None);

        return topics;
    }
}
