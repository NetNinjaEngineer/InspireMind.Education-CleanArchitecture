namespace InspireMind.Education.MVC.Helpers;

public static class Constants
{
    public const string AuthClient = "AuthClient";
    public const string CoursesClient = "CoursesClient";
    public const string TopicsClient = "TopicsClient";
    public const string LoginEndPointUri = "api/account/login";
    public const string ForgetPasswordEndPointUri = "api/account/forget-password";
    public const string ResetPasswordEndPointUri = "api/account/reset-password";
    public const string TopicsEndPointUri = "api/topics";
    public const string RegisterEndPointUri = "api/account/register";
    public const string RequestConfirmEmailEndPointUri = "api/account/request-confirm-email";
    public const string ConfirmEmailEndPointUri = "api/account/confirm-email";
    public static List<string> PageSizes = ["5", "10", "15", "20", "50"];
}
