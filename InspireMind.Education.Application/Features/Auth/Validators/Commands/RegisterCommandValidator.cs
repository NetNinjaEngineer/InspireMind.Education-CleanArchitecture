using FluentValidation;
using InspireMind.Education.Application.Features.Auth.Requests.Commands;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Auth.Validators.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IStringLocalizer<RegisterCommandValidator> _localizer;

    public RegisterCommandValidator(IStringLocalizer<RegisterCommandValidator> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.RegisterModel.FirstName)
            .NotNull().WithMessage(_localizer["notNull", "First name"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "First name"])
            .MaximumLength(50).WithMessage(_localizer["maxLength", "First name", 50]);


        RuleFor(x => x.RegisterModel.LastName)
            .NotNull().WithMessage(_localizer["notNull", "Last name"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "Last name"])
            .MaximumLength(50).WithMessage(_localizer["maxLength", "Last name", 50]);

        RuleFor(x => x.RegisterModel.UserName)
            .NotNull().WithMessage(_localizer["notNull", "Username"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "Username"]);

        RuleFor(x => x.RegisterModel.Email)
            .NotNull().WithMessage(_localizer["notNull", "Email"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "Email"]);

        RuleFor(x => x.RegisterModel.Password)
            .NotNull().WithMessage(_localizer["notNull", "Password"])
            .NotEmpty().WithMessage(_localizer["notEmpty", "Password"])
            .MinimumLength(6).WithMessage(_localizer["minLength", "Password", 6])
            .MaximumLength(100).WithMessage(_localizer["maxLength", "Password", 100]);

    }
}