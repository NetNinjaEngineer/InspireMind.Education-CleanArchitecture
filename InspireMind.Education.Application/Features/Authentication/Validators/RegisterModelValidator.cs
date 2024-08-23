using FluentValidation;
using InspireMind.Education.Application.Models.Identity;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Authentication.Validators;
public class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    private readonly IStringLocalizer<RegisterModelValidator> _localizer;

    public RegisterModelValidator(IStringLocalizer<RegisterModelValidator> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.FirstName)
            .NotNull().WithMessage(string.Format(_localizer["notNull"], "First name"))
            .NotEmpty().WithMessage(string.Format(_localizer["notEmpty"], "First name"))
            .MaximumLength(50).WithMessage(string.Format(_localizer["maxLength"], "First name", 50));


        RuleFor(x => x.LastName)
            .NotNull().WithMessage(string.Format(_localizer["notNull"], "Last name"))
            .NotEmpty().WithMessage(string.Format(_localizer["notEmpty"], "Last name"))
            .MaximumLength(50).WithMessage(string.Format(_localizer["maxLength"], "Last name", 50));

        RuleFor(x => x.UserName)
            .NotNull().WithMessage(string.Format(_localizer["notNull"], "Username"))
            .NotEmpty().WithMessage(string.Format(_localizer["notEmpty"], "Username"));

        RuleFor(x => x.Email)
            .NotNull().WithMessage(string.Format(_localizer["notNull"], "Email"))
            .NotEmpty().WithMessage(string.Format(_localizer["notEmpty"], "Email"));

        RuleFor(x => x.Password)
            .NotNull().WithMessage(string.Format(_localizer["notNull", "Password"]))
            .NotEmpty().WithMessage(string.Format(_localizer["notEmpty"], "Password"))
            .MinimumLength(6).WithMessage(string.Format(_localizer["minLength"], "Password", 6))
            .MaximumLength(100).WithMessage(string.Format(_localizer["maxLength"], 100));
    }
}
