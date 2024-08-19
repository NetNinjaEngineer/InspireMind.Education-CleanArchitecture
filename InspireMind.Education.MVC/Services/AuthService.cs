using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Helpers;
using InspireMind.Education.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InspireMind.Education.MVC.Services;

public class AuthService(
    IHttpClientFactory httpClientFactory,
    ILogger<AuthService> logger,
    IHttpContextAccessor context) : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<AuthService> _logger = logger;
    private JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<LoginResultVM> LoginAsync(LoginVM loginModel)
    {
        var client = _httpClientFactory.CreateClient(Constants.AuthClient);
        var responseMessage = await client.PostAsJsonAsync(
           requestUri: Constants.LoginEndPointUri,
           value: loginModel,
           cancellationToken: CancellationToken.None);

        var responseBody = await responseMessage.Content.ReadAsStringAsync();
        var responseBodyData = JsonSerializer.Deserialize<Result<LoginResult>>(responseBody, _jsonSerializerOptions);
        if (responseMessage.IsSuccessStatusCode && responseBodyData!.Succeeded)
        {
            _logger.LogInformation("Login successful");
            var jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(responseBodyData.Data.Token);

            var user = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await context.HttpContext!.SignInAsync(user, new AuthenticationProperties
            {
                IsPersistent = true
            });

            context.HttpContext?.Response.Cookies.Append(
                key: "token",
                value: responseBodyData.Data.Token,
                options: new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(3)
                });

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

    public async Task<bool> RegisterAsync(RegisterVM registerModel)
    {
        var client = _httpClientFactory.CreateClient(Constants.AuthClient);

        var responseMessage = await client.PostAsJsonAsync(
            requestUri: Constants.RegisterEndPointUri,
            value: registerModel,
            cancellationToken: CancellationToken.None);

        var responseBody = await responseMessage.Content.ReadAsStringAsync();

        if (responseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation("Registration successful: {ResponseBody}", responseBody);
            return true;
        }
        else
        {
            _logger.LogWarning("Registration failed: {ResponseBody}", responseBody);
            return false;
        }
    }

    public async Task<RequestConfirmEmailResult> RequestConfirmEmailAsync(RequestConfirmEmailVM requestConfirmEmailVM)
    {
        var client = _httpClientFactory.CreateClient(Constants.AuthClient);
        var responseMessage = await client.PostAsJsonAsync(
           requestUri: Constants.RequestConfirmEmailEndPointUri,
           value: requestConfirmEmailVM,
           cancellationToken: CancellationToken.None);

        var responseBody = await responseMessage.Content.ReadAsStringAsync();

        var response = JsonSerializer.Deserialize<Result<string>>(responseBody, _jsonSerializerOptions)!;

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
}
