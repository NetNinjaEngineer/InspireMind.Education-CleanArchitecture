namespace InspireMind.Education.Application.Features.Authentication.Validators;
using FluentValidation;
using InspireMind.Education.Application.Models.Identity;

public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordModelValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("The Password must be at least 6 characters long.")
            .MaximumLength(100).WithMessage("The Password must be at most 100 characters long.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Password).WithMessage("The password and confirmation password do not match.");
    }
}
