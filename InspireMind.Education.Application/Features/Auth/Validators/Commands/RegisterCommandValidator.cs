using FluentValidation;
using InspireMind.Education.Application.Models.Identity;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Auth.Validators;
public class RegisterCommandValidator : AbstractValidator<RegisterModel>
{
    private readonly IStringLocalizer<RegisterCommandValidator> _localizer;

    public RegisterCommandValidator(IStringLocalizer<RegisterCommandValidator> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.FirstName)
            .NotNull().WithMessage(_localizer["notNull", "First name"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "First name"])
            .MaximumLength(50).WithMessage(_localizer["maxLength", "First name", 50]);


        RuleFor(x => x.LastName)
            .NotNull().WithMessage(_localizer["notNull", "Last name"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "Last name"])
            .MaximumLength(50).WithMessage(_localizer["maxLength", "Last name", 50]);

        RuleFor(x => x.UserName)
            .NotNull().WithMessage(_localizer["notNull", "Username"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "Username"]);

        RuleFor(x => x.Email)
            .NotNull().WithMessage(_localizer["notNull", "Email"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "Email"]);

        RuleFor(x => x.Password)
            .NotNull().WithMessage(_localizer["notNull", "Password"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "Password"])
            .MinimumLength(6).WithMessage(_localizer["minLength", "Password", 6])
            .MaximumLength(100).WithMessage(_localizer["maxLength", 100]);
    }
}
