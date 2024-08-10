using System.Text.Json.Serialization;

namespace InspireMind.Education.Application.RequestParams;

public class CourseRequestParameters : RequestParameters
{
    public string? TopicId { get; set; }
    public CourseOrderingOptions? OrderingOptions { get; set; } = null;
}

[Flags]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CourseOrderingOptions
{
    DurationAsc = 0,
    DurationDesc = 1,
    CourseNameAsc = 2,
    CourseNameDesc = 4,
    NameAscDurationDesc = CourseNameAsc | DurationDesc,
    NameDescDurationAsc = CourseNameDesc | DurationAsc
}

