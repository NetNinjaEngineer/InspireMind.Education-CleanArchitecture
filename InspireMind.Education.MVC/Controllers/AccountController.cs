using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Helpers;
using InspireMind.Education.MVC.Models;
using InspireMind.Education.MVC.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.MVC.Controllers;
public class AccountController(
    IAuthService authService,
    IConfiguration configuration) : Controller
{
    #region Login
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
    #endregion

    #region Register
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM registerRequest)
    {
        var result = await authService.RegisterAsync(registerRequest);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction(nameof(Login));

    }
    #endregion

    #region Confirm Email
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
    #endregion

    #region Logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await authService.Logout();
        return LocalRedirect("/Account/Login");
    }
    #endregion

    #region Forget password
    [HttpGet]
    public IActionResult ForgetPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
    {
        if (ModelState.IsValid)
        {
            var result = await authService.ForgetPassword(forgetPasswordModel);

            if (result.Succeeded)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction(nameof(ForgetPasswordConfirmation));
            }

            AddErrorsToModelState(result);
        }

        return View(forgetPasswordModel);
    }

    public IActionResult ForgetPasswordConfirmation()
    {
        return View();
    }

    #endregion

    #region Reset Password

    [HttpGet]
    public IActionResult ResetPassword([FromQuery] string email, [FromQuery] string token)
    {
        if (IsEmailAndTokenValid(email, token))
        {
            HttpContext.Session.SetString("email", email);
            HttpContext.Session.SetString("token", token);
            return View();
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordModel resetPasswordModel)
    {
        string email = HttpContext.Session.GetString("email");
        string token = HttpContext.Session.GetString("token");

        if (email == null || token == null)
        {
            ModelState.AddModelError(string.Empty, "Session has timed out. Please start the process again.");
            return View("SessionTimeout");
        }

        if (IsEmailAndTokenValid(email, token))
        {
            var result = await authService.ResetPassword(email, token, resetPasswordModel);

            if (result.Succeeded)
            {
                HttpContext.Session.Clear();
                return RedirectToAction(nameof(Login));
            }

            AddErrorsToModelState(result);
        }

        return View();
    }

    private bool IsEmailAndTokenValid(string email, string token)
    {
        return !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(token);
    }

    private void AddErrorsToModelState(Result<string> result)
    {
        if (!string.IsNullOrEmpty(result.Message))
        {
            ModelState.AddModelError(Guid.NewGuid().ToString()[..5], result.Message);
        }

        if (result.Errors is not null)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(Guid.NewGuid().ToString()[..5], error);
            }
        }
    }


    #endregion

}
