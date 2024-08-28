using FluentValidation;
using InspireMind.Education.Application.Features.Users.Requests.Commands;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Users.Validators.Commands;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IStringLocalizer<UpdateUserCommandValidator> _localizer;

    public UpdateUserCommandValidator(IStringLocalizer<UpdateUserCommandValidator> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.User.FirstName)
          .NotNull().WithMessage(_localizer["notNull"])
          .NotEmpty().WithMessage(_localizer["notEmpty"])
          .MaximumLength(50).WithMessage(_localizer["maxLength", 50]);


        RuleFor(x => x.User.LastName)
            .NotNull().WithMessage(_localizer["notNull"])
            .NotEmpty().WithMessage(_localizer["notEmpty"])
            .MaximumLength(50).WithMessage(_localizer["maxLength", 50]);

        RuleFor(x => x.User.UserName)
            .NotNull().WithMessage(_localizer["notNull"])
            .NotEmpty().WithMessage(_localizer["notEmpty"]);

        RuleFor(x => x.User.Email)
            .NotNull().WithMessage(_localizer["notNull"])
            .NotEmpty().WithMessage(_localizer["notEmpty"]);
    }
}
