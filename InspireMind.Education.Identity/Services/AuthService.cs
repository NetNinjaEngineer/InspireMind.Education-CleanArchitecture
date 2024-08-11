using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Features.Authentication.Handlers.Result;
using InspireMind.Education.Application.Features.Authentication.Requests.Commands;
using InspireMind.Education.Application.Models.Identity;
using InspireMind.Education.Identity.Entities;
using InspireMind.Education.Identity.Helpers;
using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _context;
    private readonly IEmailsService _emailsService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IStringLocalizer<BaseResponseHandler> localizer,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IOptions<JwtSettings> jwtSettings,
        IHttpContextAccessor context,
        IEmailsService emailsService,
        ILogger<AuthService> logger) : base(localizer)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
        _jwtSecurityTokenHandler = new();
        _context = context;
        _emailsService = emailsService;
        _logger = logger;
    }

    public async Task<Result<LoginResult>> Login(LoginCommand request)
    {
        var loggedInUser = await _userManager.FindByEmailAsync(request.Email);

        if (loggedInUser is null || !await _userManager.CheckPasswordAsync(loggedInUser, request.Password))
            return BadRequest<LoginResult>("Invalid credientials.");

        await _signInManager.PasswordSignInAsync(
             user: loggedInUser,
             password: request.Password,
             isPersistent: true,
             lockoutOnFailure: false);

        var token = await GenerateJwtToken(loggedInUser);

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

    public async Task<Result<RegisterResult>> Register(RegisterCommand request)
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
                EmailConfirmed = true
            };

            var identityResult = await _userManager.CreateAsync(appUser, request.Password!);
            await _userManager.AddToRoleAsync(appUser, Roles.User);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(e => e.Description).ToList();
                return BadRequest<RegisterResult>("You have an validation errors.", errors: errors);
            }
        }

        return Conflict<RegisterResult>("Invalid Credientials.");

    }

    public async Task<Result<string>> ForgetPassword(ForgetPasswordModel forgetModel)
    {
        var user = await _userManager.FindByEmailAsync(forgetModel.Email);

        if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            _logger.LogWarning($"ForgotPassword failed: Invalid user for email {forgetModel.Email}");
            return BadRequest<string>("Invalid user this email");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var parameters = new Dictionary<string, string>
        {
            { "email", user.Email! },
            { "token", token }
        };

        //  $"{Request.Scheme}://{Request.Host}/api/account/reset-password?token={token}&email={model.Email}";
        var Request = _context.HttpContext!.Request;

        var callbackUri = QueryHelpers.AddQueryString($"{Request.Scheme}://{Request.Host}/api/account/reset-password", parameters!);

        var emailBody = $@"
            <p>Hello {user.UserName},</p>
            <p>You have requested to reset your password. Please click the link below to reset your password:</p>
            <p><a href='{callbackUri}'>Reset Password</a></p>
            <p>If you did not request this, please ignore this email.</p>
            <p>Thanks,<br>EduCompany</p>";

        var emailMessage = new Email
        {
            To = user.Email!,
            Subject = "Reset Password",
            Body = emailBody
        };

        Result<bool> emailSentResult = await _emailsService.SendEmail(emailMessage);

        if (emailSentResult.Succeeded)
            return Success("Reset password link has been sent.");

        _logger.LogError($"Failed to send password reset email to {user.Email}");
        return BadRequest<string>("Failed to send reset password link. Please try again later.");
    }

    public async Task<Result<string>> ResetPassword(ResetPasswordModel resetModel)
    {
        var user = await _userManager.FindByEmailAsync(resetModel.Email);

        if (user is null)
        {
            _logger.LogWarning($"ResetPassword failed: Invalid user for email {resetModel.Email}");
            return BadRequest<string>("Invalid user this email");
        }

        var result = await _userManager.ResetPasswordAsync(user, resetModel.Token, resetModel.Password);

        if (result.Succeeded)
        {
            // send email
            _logger.LogInformation($"Your password has been reset for email : '{resetModel.Email}'");


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

            return Success("Your password has been reset this email");
        }

        _logger.LogError($"Password reset failed.");

        return BadRequest<string>(
            message: "Failed to reset your password.",
            errors: result.Errors.Select(e => e.Description).ToList());
    }
}
