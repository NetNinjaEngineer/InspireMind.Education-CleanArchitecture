using FluentValidation;
using InspireMind.Education.Application.Features.Auth.Requests.Commands;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Auth.Validators.Commands;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    private readonly IStringLocalizer<ResetPasswordCommandValidator> _localizer;
    public ResetPasswordCommandValidator(IStringLocalizer<ResetPasswordCommandValidator> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.ResetRequest.Password)
           .NotNull().WithMessage(string.Format(_localizer["notNull", "Password"]))
           .NotEmpty().WithMessage(string.Format(_localizer["notEmpty"], "Password"))
           .MinimumLength(6).WithMessage(string.Format(_localizer["minLength"], "Password", 6))
           .MaximumLength(100).WithMessage(string.Format(_localizer["maxLength"], 100));

        RuleFor(x => x.ResetRequest.ConfirmPassword)
            .NotNull().WithMessage(string.Format(_localizer["notNull", "Confirm Password"]))
            .NotEmpty().WithMessage(string.Format(_localizer["notEmpty"], "Confirm Password"))
            .Equal(x => x.ResetRequest.Password).WithMessage(string.Format(_localizer["matched"], "Password", "Confirmation password"));

    }
}
