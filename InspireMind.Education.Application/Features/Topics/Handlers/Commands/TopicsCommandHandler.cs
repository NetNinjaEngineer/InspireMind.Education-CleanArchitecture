using AutoMapper;
using FluentValidation;
using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Application.DTOs.Topic;
using InspireMind.Education.Application.Exceptions;
using InspireMind.Education.Application.Features.Topics.Requests.Commands;
using InspireMind.Education.Application.Wrappers;
using InspireMind.Education.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Topics.Handlers.Commands;
public sealed class TopicsCommandHandler :
    IRequestHandler<CreateTopicCommand, Result<TopicDto>>,
    IRequestHandler<UpdateTopicCommand, Unit>,
    IRequestHandler<DeleteTopicCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<TopicForCreationDto> _createTopicValidator;
    private readonly IValidator<TopicForUpdateDto> _updateTopicValidator;
    private readonly IStringLocalizer<TopicsCommandHandler> _localizer;

    public TopicsCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IValidator<TopicForCreationDto> createTopicValidator,
        IStringLocalizer<TopicsCommandHandler> localizer)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _createTopicValidator = createTopicValidator;
        _localizer = localizer;
    }

    public async Task<Result<TopicDto>> Handle(
        CreateTopicCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _createTopicValidator.ValidateAsync(request.Topic, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var mappedResult = _mapper.Map<Topic>(request.Topic);
        _unitOfWork.Repository<Topic>()!.Create(mappedResult);

        await _unitOfWork.SaveAsync();

        return Result<TopicDto>.Success(_mapper.Map<TopicDto>(mappedResult));
    }

    public async Task<Unit> Handle(
        UpdateTopicCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _updateTopicValidator.ValidateAsync(request.UpdatedTopic, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingTopic = await _unitOfWork.Repository<Topic>()!.GetEntityAsync(request.TopicId)
              ?? throw new NotFoundException(_localizer[SharedResourcesKeys.TopicNotFoundMessage, request.TopicId]);

        _mapper.Map(request.UpdatedTopic, existingTopic);

        _unitOfWork.Repository<Topic>()!.Update(existingTopic);

        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }

    public async Task<Unit> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        var existingTopic = await _unitOfWork.Repository<Topic>()!.GetEntityAsync(request.TopicId)
                   ?? throw new NotFoundException(_localizer[SharedResourcesKeys.TopicNotFoundMessage, request.TopicId]);

        _unitOfWork.Repository<Topic>()!.Delete(existingTopic);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
