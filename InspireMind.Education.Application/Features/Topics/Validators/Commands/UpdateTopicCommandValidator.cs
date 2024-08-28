using FluentValidation;
using InspireMind.Education.Application.Features.Topics.Requests.Commands;
using InspireMind.Education.Application.Wrappers;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Topics.Validators.Commands;
public sealed class UpdateTopicCommandValidator : AbstractValidator<UpdateTopicCommand>
{
    private readonly IStringLocalizer<UpdateTopicCommandValidator> _localizer;

    public UpdateTopicCommandValidator(IStringLocalizer<UpdateTopicCommandValidator> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.UpdatedTopic.TopicName)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
            .MaximumLength(50).WithMessage(_localizer[SharedResourcesKeys.MaximumLength50]);
    }
}
