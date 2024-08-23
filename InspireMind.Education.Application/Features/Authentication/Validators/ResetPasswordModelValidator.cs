namespace InspireMind.Education.Application.Features.Authentication.Validators;
using FluentValidation;
using InspireMind.Education.Application.Models.Identity;
using Microsoft.Extensions.Localization;

public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
{
    private readonly IStringLocalizer<ResetPasswordModelValidator> _localizer;
    public ResetPasswordModelValidator(IStringLocalizer<ResetPasswordModelValidator> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.Password)
           .NotNull().WithMessage(string.Format(_localizer["notNull", "Password"]))
           .NotEmpty().WithMessage(string.Format(_localizer["notEmpty"], "Password"))
           .MinimumLength(6).WithMessage(string.Format(_localizer["minLength"], "Password", 6))
           .MaximumLength(100).WithMessage(string.Format(_localizer["maxLength"], 100));

        RuleFor(x => x.ConfirmPassword)
            .NotNull().WithMessage(string.Format(_localizer["notNull", "Confirm Password"]))
            .NotEmpty().WithMessage(string.Format(_localizer["notEmpty"], "Confirm Password"))
            .Equal(x => x.Password).WithMessage(string.Format(_localizer["matched"], "Password", "Confirmation password"));

    }
}
