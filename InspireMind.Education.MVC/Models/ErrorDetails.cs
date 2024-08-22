using System.Text.Json;

namespace InspireMind.Education.MVC.Models;

public sealed class ErrorDetails(int statusCode, IEnumerable<string> errors, string? description = null)
{
    public int StatusCode { get; set; } = statusCode;
    public IEnumerable<string> Errors { get; set; } = errors;
    public string? Description { get; set; } = description;

    public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
}
