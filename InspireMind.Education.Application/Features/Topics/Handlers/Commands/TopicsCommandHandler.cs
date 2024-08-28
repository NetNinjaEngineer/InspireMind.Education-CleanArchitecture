using AutoMapper;
using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Application.Exceptions;
using InspireMind.Education.Application.Features.Topics.DTOs;
using InspireMind.Education.Application.Features.Topics.Requests.Commands;
using InspireMind.Education.Application.Wrappers;
using InspireMind.Education.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Topics.Handlers.Commands;
public sealed class TopicsCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IStringLocalizer<TopicsCommandHandler> localizer)
    : IRequestHandler<CreateTopicCommand, Result<TopicDto>>,
        IRequestHandler<UpdateTopicCommand, Unit>,
        IRequestHandler<DeleteTopicCommand, Unit>
{
    public async Task<Result<TopicDto>> Handle(
        CreateTopicCommand request,
        CancellationToken cancellationToken)
    {
        var mappedResult = mapper.Map<Topic>(request.Topic);
        unitOfWork.Repository<Topic>()!.Create(mappedResult);

        await unitOfWork.SaveAsync();

        return Result<TopicDto>.Success(mapper.Map<TopicDto>(mappedResult));
    }

    public async Task<Unit> Handle(
        UpdateTopicCommand request,
        CancellationToken cancellationToken)
    {
        var existingTopic = await unitOfWork.Repository<Topic>()!.GetEntityAsync(request.TopicId)
              ?? throw new NotFoundException(localizer[SharedResourcesKeys.TopicNotFoundMessage, request.TopicId]);

        mapper.Map(request.UpdatedTopic, existingTopic);

        unitOfWork.Repository<Topic>()!.Update(existingTopic);

        await unitOfWork.SaveAsync();

        return Unit.Value;
    }

    public async Task<Unit> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        var existingTopic = await unitOfWork.Repository<Topic>()!.GetEntityAsync(request.TopicId)
                   ?? throw new NotFoundException(localizer[SharedResourcesKeys.TopicNotFoundMessage, request.TopicId]);

        unitOfWork.Repository<Topic>()!.Delete(existingTopic);
        await unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
