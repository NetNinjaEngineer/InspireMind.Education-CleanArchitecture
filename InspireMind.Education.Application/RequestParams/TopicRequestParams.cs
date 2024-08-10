using System.Text.Json.Serialization;

namespace InspireMind.Education.Application.RequestParams;
public class TopicRequestParams : RequestParameters
{
    public TopicOrderingOptions? OrderingOptions { get; set; } = null;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TopicOrderingOptions
{
    NameAsc,
    NameDesc
}
