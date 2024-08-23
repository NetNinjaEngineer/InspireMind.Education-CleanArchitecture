using FluentValidation;
using InspireMind.Education.Application.DTOs.User;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Users.Validators;
public class UserForUpdateDtoValidator : AbstractValidator<UserForUpdateDto>
{
    private readonly IStringLocalizer<UserForUpdateDtoValidator> _localizer;

    public UserForUpdateDtoValidator(IStringLocalizer<UserForUpdateDtoValidator> localizer)
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
    }
}
