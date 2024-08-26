using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Features.Auth.Handlers.Result;
using InspireMind.Education.Application.Models.Identity;
using InspireMind.Education.Identity.Entities;
using InspireMind.Education.Identity.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InspireMind.Education.Identity.Services;
public class AuthService : BaseResponseHandler, IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly IEmailsService _emailsService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IStringLocalizer<BaseResponseHandler> localizer,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IOptions<JwtSettings> jwtSettings,
        IEmailsService emailsService,
        ILogger<AuthService> logger) : base(localizer)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
        _jwtSecurityTokenHandler = new();
        _emailsService = emailsService;
        _logger = logger;
    }

    public async Task<Result<LoginResult>> Login(LoginModel request)
    {
        var loggedInUser = await _userManager.FindByEmailAsync(request.Email);

        if (loggedInUser is null)
            return BadRequest<LoginResult>(_localizer["UnknownUser"]);

        if (!await _userManager.IsEmailConfirmedAsync(loggedInUser))
            return BadRequest<LoginResult>(_localizer["EmailNotConfirmed"]);

        if (!await _userManager.CheckPasswordAsync(loggedInUser, request.Password))
            return BadRequest<LoginResult>(_localizer["InvalidCredientials"]);

        await _signInManager.PasswordSignInAsync(
             user: loggedInUser,
             password: request.Password,
             isPersistent: true,
             lockoutOnFailure: false);

        var token = await GenerateJwtToken(loggedInUser);

        _logger.LogInformation($"Login successfull.");

        return Success(new LoginResult(
            isSucessfull: true,
            token: _jwtSecurityTokenHandler.WriteToken(token),
            userId: loggedInUser.Id!,
            email: loggedInUser.Email!,
            username: loggedInUser.UserName!
        ));
    }

    private async Task<JwtSecurityToken> GenerateJwtToken(AppUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(CustomClaimTypes.Uid, user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

    public async Task<Result<RegisterResult>> Register(RegisterModel request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is not null)
        {
            return Conflict<RegisterResult>(_localizer["UsernameExisted"]);
        }

        var existingUserEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existingUserEmail is null)
        {
            var appUser = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                EmailConfirmed = false
            };

            var identityResult = await _userManager.CreateAsync(appUser, request.Password!);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(e => e.Description).ToList();
                return BadRequest<RegisterResult>(_localizer["validationErrors"], errors: errors);
            }

            await _userManager.AddToRoleAsync(appUser, Roles.User);

            _logger.LogInformation($"Register successfull.");
            return Created(new RegisterResult(true));
        }

        return Conflict<RegisterResult>(_localizer["EmailTaken"]);

    }

    public async Task<Result<string>> ForgetPassword(ForgetPasswordModel forgetModel)
    {
        var user = await _userManager.FindByEmailAsync(forgetModel.Email);

        if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            _logger.LogWarning($"ForgotPassword failed: Invalid user for email {forgetModel.Email}");
            return BadRequest<string>(_localizer["UnknownUser"]);
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

        var parameters = new Dictionary<string, string>
        {
            { "email", user.Email! },
            { "token", encodedToken }
        };

        //  $"{Request.Scheme}://{Request.Host}/api/account/reset-password?token={token}&email={model.Email}";
        //var Request = _context.HttpContext!.Request;
        var resetPasswordUri = $"{forgetModel.ClientUri}/Account/ResetPassword";

        var callbackUri = QueryHelpers.AddQueryString($"{resetPasswordUri}", parameters!);

        var emailBody = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #1e1e2f; border-radius: 8px; color: #e1e1e1;'>
                <h2 style='color: #ffffff; text-align: center; font-size: 24px; animation: fadeIn 2s;'>Hello {user.UserName},</h2>
                <p style='color: #dcdcdc; line-height: 1.6; font-size: 16px; text-align: center;'>
                    You have requested to reset your password. Please click the button below to proceed:
                </p>
                <p style='text-align: center; margin-top: 30px;'>
                    <a href='{callbackUri}' style='background-color: #FF5733; color: white; padding: 12px 20px; text-decoration: none; font-size: 16px; border-radius: 4px; transition: background-color 0.3s ease-in-out; display: inline-block;'>
                        Reset Password
                    </a>
                </p>
                <p style='color: #dcdcdc; line-height: 1.6; font-size: 14px; text-align: center; margin-top: 30px;'>
                    If you did not request this, please ignore this email.
                </p>
                <p style='color: #999; line-height: 1.6; font-size: 14px; text-align: center; margin-top: 40px;'>
                    Thanks
                </p>
                <div style='border-top: 1px solid #333; padding-top: 20px; margin-top: 40px;'>
                    <p style='color: #555; font-size: 12px; text-align: center;'>© 2024 EduCompany. All rights reserved.</p>
                </div>
            </div>
            <style>
                @keyframes fadeIn {{
                    0% {{ opacity: 0; }}
                    100% {{ opacity: 1; }}
                }}

                a:hover {{
                    background-color: #E74C3C;
                }}
            </style>";


        var emailMessage = new Email
        {
            To = user.Email!,
            Subject = "Reset Password",
            Body = emailBody
        };

        Result<bool> emailSentResult = await _emailsService.SendEmail(emailMessage);

        if (emailSentResult.Succeeded)
            return Success<string>(_localizer["ResetPasswordLinkSent"]);

        _logger.LogError($"Failed to send password reset email to {user.Email}");
        return BadRequest<string>(_localizer["ResetPasswordLinkFailure"]);
    }

    public async Task<Result<string>> ResetPassword(string email, string token, ResetPasswordModel resetModel)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            _logger.LogWarning($"ResetPassword failed: Invalid user for email {email}");
            return BadRequest<string>(_localizer["UnknownUser"]);
        }

        var result = await _userManager.ResetPasswordAsync(user, token, resetModel.Password);

        if (result.Succeeded)
        {
            // send email
            _logger.LogInformation($"Your password has been reset for email : '{email}'");


            var emailBody = $@"
            <p>Hello {user.UserName},</p>
            <p>Your password has been reset this email {user.Email}";

            var emailMessage = new Email
            {
                To = user.Email!,
                Subject = "Password Reset Successfully.",
                Body = emailBody
            };

            await _emailsService.SendEmail(emailMessage);

            return Success<string>(_localizer["PasswordResetSuccess"]);
        }

        _logger.LogError($"Password reset failed.");

        return BadRequest<string>(
            message: _localizer["PasswordResetFailure"],
            errors: result.Errors.Select(e => e.Description).ToList());
    }

    public async Task<Result<string>> RequestConfirmEmail(RequestConfirmEmailModel requestConfirmModel)
    {
        var user = await _userManager.FindByEmailAsync(requestConfirmModel.Email);

        if (user is null)
        {
            _logger.LogError($"Invalid user requested.");
            return BadRequest<string>(_localizer["UnknownUser"]);
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

        var emailConfirmationParams = new Dictionary<string, string>
        {
            { "email", user.Email! },
            {"token", encodedToken}
        };

        var confirmUri = $"{requestConfirmModel.ClientUri}/Account/ConfirmEmail";

        var callbackUri = QueryHelpers.AddQueryString(confirmUri, emailConfirmationParams!);

        var emailBody = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #1e1e2f; border-radius: 8px; color: #e1e1e1;'>
                <h2 style='color: #ffffff; text-align: center; font-size: 24px; animation: fadeIn 2s;'>Hello {user.UserName},</h2>
                <p style='color: #dcdcdc; line-height: 1.6; font-size: 16px; text-align: center;'>
                    You have requested to confirm your email. Please click the button below to confirm it:
                </p>
                <p style='text-align: center; margin-top: 30px;'>
                    <a href='{callbackUri}' style='background-color: #4CAF50; color: white; padding: 12px 20px; text-decoration: none; font-size: 16px; border-radius: 4px; transition: background-color 0.3s ease-in-out; display: inline-block;'>
                        Confirm Email
                    </a>
                </p>
                <p style='color: #dcdcdc; line-height: 1.6; font-size: 14px; text-align: center; margin-top: 30px;'>
                    If you didn't request this, please ignore this email.
                </p>
                <div style='border-top: 1px solid #333; padding-top: 20px; margin-top: 40px;'>
                    <p style='color: #555; font-size: 12px; text-align: center;'>Thank you for using our service!</p>
                    <p style='color: #555; font-size: 12px; text-align: center;'>© 2024 Your Company. All rights reserved.</p>
                </div>
            </div>
            <style>
                @keyframes fadeIn {{
                    0% {{ opacity: 0; }}
                    100% {{ opacity: 1; }}
                }}

                a:hover {{
                    background-color: #3e8e41;
                }}
            </style>";


        var emailMessage = new Email
        {
            To = user.Email!,
            Subject = "Confirm your email",
            Body = emailBody
        };

        var status = await _emailsService.SendEmail(emailMessage);

        return status.Succeeded ?
            Success<string>(_localizer["EmailConfirmationMessageSent"]) :
            BadRequest<string>(_localizer["EmailConfirmationMessageNotSent"]);

    }

    public async Task<Result<string>> ConfirmEmail(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is not null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded ?
                Success<string>(_localizer["EmailConfirmed", user.Email!]) :
                BadRequest<string>(_localizer["EmailNotConfirmed", user.Email!]);
        }

        return BadRequest<string>(_localizer["UnknownUser"]);
    }
}
