using AutoMapper;
using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Application.Exceptions;
using InspireMind.Education.Application.Features.Topics.DTOs;
using InspireMind.Education.Application.Features.Topics.Requests.Queries;
using InspireMind.Education.Application.Specifications;
using InspireMind.Education.Application.Wrappers;
using InspireMind.Education.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Topics.Handlers.Queries;
public sealed class TopicsQueryHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IStringLocalizer<TopicsQueryHandler> localizer)
    : IRequestHandler<GetAllTopicsQuery, Result<IReadOnlyList<TopicDto>>>,
        IRequestHandler<GetAllTopicsWithParamsQuery, Result<Pagination<TopicDto>>>,
        IRequestHandler<GetTopicWithRelatedCoursesQuery, Result<TopicWithRelatedCoursesDto>>,
        IRequestHandler<GetSingleTopicQuery, Result<TopicDto>>
{
    public async Task<Result<IReadOnlyList<TopicDto>>> Handle(
        GetAllTopicsQuery request,
        CancellationToken cancellationToken)
    {
        return Result<IReadOnlyList<TopicDto>>.Success(
                mapper.Map<IReadOnlyList<TopicDto>>(await unitOfWork.Repository<Topic>()!.GetAllAsync()));
    }

    public async Task<Result<Pagination<TopicDto>>> Handle(
        GetAllTopicsWithParamsQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new GetAllTopicsWithCoursesSpecification(request.TopicRequestParams);

        var topics = await unitOfWork.Repository<Topic>()!.GetAllWithSpecificationAsync(specification);

        var mappedTopics = mapper.Map<IReadOnlyList<TopicDto>>(topics);
        var specificationCountWithFilteration = new GetTopicsCountWithFilterationSpecification(request.TopicRequestParams);
        var count = await unitOfWork.Repository<Topic>()!.CountWithSpecificationAsync(specificationCountWithFilteration);

        return Result<Pagination<TopicDto>>.Success(Pagination<TopicDto>.ToPaginatedResult(
            request.TopicRequestParams.PageNumber,
            request.TopicRequestParams.PageSize,
            count,
            mappedTopics));
    }

    public async Task<Result<TopicWithRelatedCoursesDto>> Handle(
        GetTopicWithRelatedCoursesQuery request,
        CancellationToken cancellationToken)
    {
        return Result<TopicWithRelatedCoursesDto>.Success(await unitOfWork.TopicRepository.GetTopicWithRelatedCourses(request.TopicId)
                   ?? throw new NotFoundException(localizer[SharedResourcesKeys.TopicNotFoundMessage, request.TopicId]));
    }

    public async Task<Result<TopicDto>> Handle(
        GetSingleTopicQuery request,
        CancellationToken cancellationToken)
    {
        var topic = await unitOfWork.Repository<Topic>()!.GetEntityAsync(request.Id)
         ?? throw new NotFoundException(localizer[SharedResourcesKeys.TopicNotFoundMessage, request.Id]);
        return Result<TopicDto>.Success(mapper.Map<Topic, TopicDto>(topic));
    }
}
