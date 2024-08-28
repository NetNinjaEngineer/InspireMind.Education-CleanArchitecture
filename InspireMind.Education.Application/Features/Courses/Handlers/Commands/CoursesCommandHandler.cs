using AutoMapper;
using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Application.Exceptions;
using InspireMind.Education.Application.Features.Courses.DTOs;
using InspireMind.Education.Application.Features.Courses.Requests.Commands;
using InspireMind.Education.Application.Wrappers;
using InspireMind.Education.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Application.Features.Courses.Handlers.Commands;
public sealed class CoursesCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IStringLocalizer<CoursesCommandHandler> localizer)
    :
        IRequestHandler<CreateCourseCommand, Result<CourseForListDto>>,
        IRequestHandler<DeleteCourseCommand, Unit>,
        IRequestHandler<UpdateCourseCommand, Unit>
{
    public async Task<Result<CourseForListDto>> Handle(
        CreateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var mappedCourse = mapper.Map<Course>(request);
        unitOfWork.Repository<Course>()!.Create(mappedCourse);

        await unitOfWork.SaveAsync();
        return Result<CourseForListDto>.Success(mapper.Map<Course, CourseForListDto>(mappedCourse));
    }

    public async Task<Unit> Handle(
        DeleteCourseCommand request,
        CancellationToken cancellationToken)
    {
        var existingCourse = await unitOfWork.Repository<Course>()!.GetEntityAsync(request.Id)
       ?? throw new NotFoundException(localizer[SharedResourcesKeys.CourseNotFoundMessage, request.Id]);

        unitOfWork.Repository<Course>()!.Delete(existingCourse);
        await unitOfWork.SaveAsync();

        return Unit.Value;
    }

    public async Task<Unit> Handle(
        UpdateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var existingCourse = await unitOfWork.Repository<Course>()!.GetEntityAsync(request.Id)
              ?? throw new NotFoundException(localizer[SharedResourcesKeys.CourseNotFoundMessage, request.Id]);

        mapper.Map(request.Course, existingCourse);

        unitOfWork.Repository<Course>()!.Update(existingCourse);

        await unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
