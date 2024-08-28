using FluentValidation;
using InspireMind.Education.Application.Features.Courses.Requests.Commands;
using InspireMind.Education.Application.Wrappers;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Courses.Validators.Commands;
public sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator(IStringLocalizer<CreateCourseCommandValidator> localizer)
    {
        RuleFor(x => x.Course.CourseName)
            .NotEmpty().WithMessage(localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(localizer[SharedResourcesKeys.NotNull])
            .MaximumLength(50).WithMessage(localizer[SharedResourcesKeys.MaximumLength50]);

        RuleFor(x => x.Course.Duration)
            .NotNull().WithMessage(localizer[SharedResourcesKeys.CourseDurationRequired])
            .GreaterThan(0).WithMessage(localizer[SharedResourcesKeys.CourseDurationGreaterThanZero]);
    }
}
