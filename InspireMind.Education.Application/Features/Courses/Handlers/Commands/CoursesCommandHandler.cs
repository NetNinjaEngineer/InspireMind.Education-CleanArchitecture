using AutoMapper;
using FluentValidation;
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
public sealed class CoursesCommandHandler :
    IRequestHandler<CreateCourseCommand, Result<CourseForListDto>>,
    IRequestHandler<DeleteCourseCommand, Unit>,
    IRequestHandler<UpdateCourseCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CourseForCreateDto> _createCourseValidator;
    private readonly IValidator<CourseForUpdateDto> _updateCourseValidator;
    private readonly IStringLocalizer<CoursesCommandHandler> _localizer;

    public CoursesCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IValidator<CourseForCreateDto> createCourseValidator,
        IValidator<CourseForUpdateDto> updateCourseValidator,
        IStringLocalizer<CoursesCommandHandler> localizer)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _createCourseValidator = createCourseValidator;
        _updateCourseValidator = updateCourseValidator;
        _localizer = localizer;
    }

    public async Task<Result<CourseForListDto>> Handle(
        CreateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _createCourseValidator.ValidateAsync(request.Course, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var mappedCourse = _mapper.Map<Course>(request.Course);
        _unitOfWork.Repository<Course>()!.Create(mappedCourse);

        await _unitOfWork.SaveAsync();
        return Result<CourseForListDto>.Success(_mapper.Map<Course, CourseForListDto>(mappedCourse));
    }

    public async Task<Unit> Handle(
        DeleteCourseCommand request,
        CancellationToken cancellationToken)
    {
        var existingCourse = await _unitOfWork.Repository<Course>()!.GetEntityAsync(request.Id)
       ?? throw new NotFoundException(_localizer[SharedResourcesKeys.CourseNotFoundMessage, request.Id]);

        _unitOfWork.Repository<Course>()!.Delete(existingCourse);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }

    public async Task<Unit> Handle(
        UpdateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _updateCourseValidator.ValidateAsync(request.Course, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingCourse = await _unitOfWork.Repository<Course>()!.GetEntityAsync(request.Id)
              ?? throw new NotFoundException(_localizer[SharedResourcesKeys.CourseNotFoundMessage, request.Id]);

        _mapper.Map(request.Course, existingCourse);

        _unitOfWork.Repository<Course>()!.Update(existingCourse);

        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
