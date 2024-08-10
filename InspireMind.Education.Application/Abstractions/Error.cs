namespace InspireMind.Education.Application.Abstractions;

public sealed class Error(IEnumerable<string>? errors)
{
    public IEnumerable<string>? ErrorMessage { get; set; } = errors;

    public static Error None => new(null);
}
