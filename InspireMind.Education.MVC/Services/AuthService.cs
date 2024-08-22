using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Helpers;
using InspireMind.Education.MVC.Models;
using InspireMind.Education.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.WebUtilities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InspireMind.Education.MVC.Services;

public class AuthService(
    IHttpClientFactory httpClientFactory,
    ILogger<AuthService> logger,
    IHttpContextAccessor context,
    ILocalStorageService localStorageService) : BaseHttpService(localStorageService), IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<AuthService> _logger = logger;
    private JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private JsonSerializerOptions _jsonSerializerSettings = new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true
    };

    public async Task<LoginResultVM> LoginAsync(LoginVM loginModel)
    {
        var client = _httpClientFactory.CreateClient(Constants.AuthClient);
        var responseMessage = await client.PostAsJsonAsync(
           requestUri: Constants.LoginEndPointUri,
           value: loginModel,
           cancellationToken: CancellationToken.None);

        var responseBody = await responseMessage.Content.ReadAsStringAsync();
        var responseBodyData = JsonSerializer.Deserialize<Result<LoginResult>>(responseBody, _jsonSerializerSettings);
        if (responseMessage.IsSuccessStatusCode && responseBodyData!.Succeeded)
        {
            _logger.LogInformation("Login successful");
            var jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(responseBodyData.Data.Token);

            var user = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await context.HttpContext!.SignInAsync(user, new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                RedirectUri = "/Account/Login"
            });

            _localStorageService.SetStorageValue("token", responseBodyData.Data.Token);

            return new LoginResultVM(
                isEmailConfirmed: true,
                isSuccessfull: true
                );
        }

        _logger.LogInformation("Login failed: {ResponseBody}", responseBody);
        return new LoginResultVM(
            isSuccessfull: false,
            isEmailConfirmed: false
            );
    }

    public async Task<Result<RegisterResult>> RegisterAsync(RegisterVM registerModel)
    {
        var client = _httpClientFactory.CreateClient(Constants.AuthClient);

        var responseMessage = await client.PostAsJsonAsync(
            requestUri: Constants.RegisterEndPointUri,
            value: registerModel,
            cancellationToken: CancellationToken.None);

        var responseBody = await responseMessage.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Result<RegisterResult>>(responseBody, _jsonSerializerSettings)!;
    }

    public async Task<RequestConfirmEmailResult> RequestConfirmEmailAsync(RequestConfirmEmailVM requestConfirmEmailVM)
    {
        var client = _httpClientFactory.CreateClient(Constants.AuthClient);
        var responseMessage = await client.PostAsJsonAsync(
           requestUri: Constants.RequestConfirmEmailEndPointUri,
           value: requestConfirmEmailVM,
           cancellationToken: CancellationToken.None);

        var responseBody = await responseMessage.Content.ReadAsStringAsync();

        var response = JsonSerializer.Deserialize<Result<string>>(responseBody, _jsonSerializerSettings)!;

        if (response.Succeeded)
        {
            return new RequestConfirmEmailResult(
                successMessage: response.Data
                );
        }

        var errorMessageBuilder = new StringBuilder();
        foreach (var error in response.Errors)
            errorMessageBuilder.AppendLine(error);


        return new RequestConfirmEmailResult(
            errorMessage: errorMessageBuilder.ToString()
            );

    }

    public async Task Logout()
    {
        _localStorageService.ClearStorage(["token"]);
        await context.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<Result<string>> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
    {
        var client = _httpClientFactory.CreateClient(Constants.AuthClient);
        var responseMessage = await client.PostAsJsonAsync(
           requestUri: Constants.ForgetPasswordEndPointUri,
           value: forgetPasswordModel,
           cancellationToken: CancellationToken.None);

        var responseBody = await responseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Result<string>>(responseBody, _jsonSerializerSettings)!;
    }

    public async Task<Result<string>> ResetPassword(string email, string token, ResetPasswordModel resetPasswordModel)
    {
        var client = _httpClientFactory.CreateClient(Constants.AuthClient);
        var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));


        var requestUri = QueryHelpers.AddQueryString(
            Constants.ResetPasswordEndPointUri,
            new Dictionary<string, string>
            {
            { "email", email },
            { "token", decodedToken }
            });

        var responseMessage = await client.PostAsJsonAsync(
            requestUri: requestUri,
            value: resetPasswordModel,
            cancellationToken: CancellationToken.None);

        var responseBody = await responseMessage.Content.ReadAsStringAsync();

        if (responseMessage.StatusCode == HttpStatusCode.UnprocessableEntity)
        {
            var errorDetails = JsonSerializer.Deserialize<ErrorDetails>(responseBody, _jsonSerializerSettings)!;
            return new Result<string>()
            {
                Errors = errorDetails.Errors.ToList(),
                StatusCode = (HttpStatusCode)errorDetails.StatusCode,
                Succeeded = false
            };
        }

        return JsonSerializer.Deserialize<Result<string>>(responseBody, _jsonSerializerSettings)!;
    }
}
