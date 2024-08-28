namespace InspireMind.Education.MVC.Models;

public class RequestConfirmEmailResult(string? successMessage = null, string? errorMessage = null)
{
    public bool IsSuccess { get; } = !string.IsNullOrEmpty(successMessage);
    public string? SuccessMessage { get; } = successMessage;
    public string? ErrorMessage { get; } = errorMessage;
}
