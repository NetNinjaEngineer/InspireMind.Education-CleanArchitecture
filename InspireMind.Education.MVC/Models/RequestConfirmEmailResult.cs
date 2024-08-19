namespace InspireMind.Education.MVC.Models;

public class RequestConfirmEmailResult
{
    public RequestConfirmEmailResult(string? successMessage = null, string? errorMessage = null)
    {
        IsSuccess = !string.IsNullOrEmpty(successMessage);
        SuccessMessage = successMessage;
        ErrorMessage = errorMessage;
    }


    public bool IsSuccess { get; }
    public string? SuccessMessage { get; }
    public string? ErrorMessage { get; }

}
