using FluentValidation;
using InspireMind.Education.Application.Features.Topics.Requests.Commands;
using InspireMind.Education.Application.Wrappers;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Topics.Validators.Commands
{
    public class CreateTopicCommandValidator : AbstractValidator<CreateTopicCommand>
    {
        private readonly IStringLocalizer<CreateTopicCommandValidator> _localizer;

        public CreateTopicCommandValidator(IStringLocalizer<CreateTopicCommandValidator> localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.Topic.TopicName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .MaximumLength(50).WithMessage(_localizer[SharedResourcesKeys.MaximumLength50]);

        }
    }
}
