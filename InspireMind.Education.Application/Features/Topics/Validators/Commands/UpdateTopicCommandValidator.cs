using FluentValidation;
using InspireMind.Education.Application.DTOs.Topic;
using InspireMind.Education.Application.Wrappers;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Topics.Validators.Commands;
public sealed class UpdateTopicCommandValidator : AbstractValidator<TopicForUpdateDto>
{
    private readonly IStringLocalizer<UpdateTopicCommandValidator> _localizer;

    public UpdateTopicCommandValidator(IStringLocalizer<UpdateTopicCommandValidator> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.TopicName)
            .NotEmpty().WithMessage(string.Format(_localizer[SharedResourcesKeys.NotEmpty], "TopicName"))
            .NotNull().WithMessage(string.Format(_localizer[SharedResourcesKeys.NotNull], "TopicName"))
            .MaximumLength(50).WithMessage(string.Format(_localizer[SharedResourcesKeys.MaximumLength50], "TopicName"));
    }
}
