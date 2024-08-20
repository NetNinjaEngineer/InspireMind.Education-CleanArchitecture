using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.MVC.Controllers;
public class AccountController(
    IAuthService authService,
    IConfiguration configuration) : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginRequest)
    {
        var loginResult = await authService.LoginAsync(loginRequest);
        if (!loginResult.IsEmailConfirmed)
        {
            return RedirectToAction("RequestConfirmEmail");
        }

        if (loginResult.IsSuccessfull)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError(string.Empty, "");
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM registerRequest)
    {
        var isRegistered = await authService.RegisterAsync(registerRequest);
        if (!isRegistered)
        {
            ModelState.AddModelError(string.Empty, "");
            return View();
        }

        return RedirectToAction("Login");
    }

    public IActionResult RequestConfirmEmail()
    {
        ViewBag.ClientUri = configuration.GetSection("ClientUri").Value!;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RequestConfirmEmail(RequestConfirmEmailVM confirmEmailRequest)
    {
        var requestConfirmEmailResponse = await authService.RequestConfirmEmailAsync(confirmEmailRequest);

        if (!requestConfirmEmailResponse.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, requestConfirmEmailResponse.ErrorMessage!);
            return View();
        }

        ViewBag.EmailConfirmationSent = requestConfirmEmailResponse.SuccessMessage;

        return View();

    }

    [HttpGet]
    public IActionResult ConfirmEmail([FromQuery] string email, [FromQuery] string token)
    {
        ViewBag.Email = email;
        ViewBag.Token = token;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await authService.Logout();
        return LocalRedirect("/Account/Login");
    }

}
