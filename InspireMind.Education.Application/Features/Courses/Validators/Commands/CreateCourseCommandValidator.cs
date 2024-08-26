using FluentValidation;
using InspireMind.Education.Application.Features.Courses.DTOs;
using InspireMind.Education.Application.Wrappers;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Courses.Validators.Commands;
public sealed class CreateCourseCommandValidator : AbstractValidator<CourseForCreateDto>
{
    private readonly IStringLocalizer<CreateCourseCommandValidator> localizer;
    public CreateCourseCommandValidator(IStringLocalizer<CreateCourseCommandValidator> localizer)
    {
        this.localizer = localizer;

        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage(string.Format(localizer[SharedResourcesKeys.NotEmpty], "CourseName"))
            .NotNull().WithMessage(string.Format(localizer[SharedResourcesKeys.NotNull], "CourseName"))
            .MaximumLength(50).WithMessage(string.Format(localizer[SharedResourcesKeys.MaximumLength50], "CourseName"));

        RuleFor(x => x.Duration)
            .NotNull().WithMessage(localizer[SharedResourcesKeys.CourseDurationRequired])
            .GreaterThan(0).WithMessage(localizer[SharedResourcesKeys.CourseDurationGreaterThanZero]);
    }
}
