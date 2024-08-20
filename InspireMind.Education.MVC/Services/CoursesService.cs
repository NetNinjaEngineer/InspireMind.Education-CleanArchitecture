using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Helpers;
using InspireMind.Education.MVC.Services.Base;
using System.Net.Http.Headers;
using System.Text;

namespace InspireMind.Education.MVC.Services;

public class CoursesService(
    IHttpClientFactory httpClientFactory,
    ILocalStorageService localStorageService)
    : BaseHttpService(localStorageService), ICoursesService
{
    public async Task<CourseDtoPagination?> GetPaginatedCourses(
        int? PageNumber,
        int? PageSize,
        Guid? TopicId,
        string? SearchTerm,
        CourseOrderingOptions? OrderingOptions)
    {
        var client = httpClientFactory.CreateClient(Constants.CoursesClient);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetJwtToken());

        StringBuilder uriBuilder = new($"api/courses/coursesWithTopics?");

        if (PageNumber.HasValue)
            uriBuilder.Append($"&pageNumber={PageNumber}");

        if (PageSize.HasValue)
            uriBuilder.Append($"&pageSize={PageSize}");

        if (!string.IsNullOrEmpty(SearchTerm))
            uriBuilder.Append($"&searchTerm={SearchTerm}");

        if (TopicId is not null)
            uriBuilder.Append($"&topicId={TopicId.Value.ToString()}");

        if (OrderingOptions is not null)
            uriBuilder.Append($"&orderingOptions={OrderingOptions.Value}");

        var pagedResult = await client.GetFromJsonAsync<CourseDtoPagination>(
            uriBuilder.ToString(),
            CancellationToken.None);

        return pagedResult;
    }
}
