using FluentValidation;
using InspireMind.Education.Application.Features.Topics.DTOs;
using InspireMind.Education.Application.Wrappers;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Topics.Validators.Commands
{
    public class CreateTopicCommandValidator : AbstractValidator<TopicForCreationDto>
    {
        private readonly IStringLocalizer<CreateTopicCommandValidator> _localizer;

        public CreateTopicCommandValidator(IStringLocalizer<CreateTopicCommandValidator> localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.TopicName)
                .NotEmpty().WithMessage(string.Format(_localizer[SharedResourcesKeys.NotEmpty], "TopicName"))
                .NotNull().WithMessage(string.Format(_localizer[SharedResourcesKeys.NotNull], "TopicName"))
                .MaximumLength(50).WithMessage(string.Format(_localizer[SharedResourcesKeys.MaximumLength50], "TopicName"));

        }
    }
}
