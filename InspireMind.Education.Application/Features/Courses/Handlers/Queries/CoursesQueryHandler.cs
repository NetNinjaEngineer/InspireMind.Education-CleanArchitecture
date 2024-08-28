using AutoMapper;
using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Application.Features.Courses.DTOs;
using InspireMind.Education.Application.Features.Courses.Requests.Queries;
using InspireMind.Education.Application.Specifications;
using InspireMind.Education.Application.Wrappers;
using InspireMind.Education.Domain.Entities;
using MediatR;

namespace InspireMind.Education.Application.Features.Courses.Handlers.Queries;
public sealed class CoursesQueryHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetCoursesListQuery, Result<IReadOnlyList<CourseForListDto>>>,
    IRequestHandler<GetCoursesWithTopicsQuery, Result<Pagination<CourseDto>>>
{
    public async Task<Result<IReadOnlyList<CourseForListDto>>> Handle(
        GetCoursesListQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await unitOfWork.Repository<Course>()!.GetAllAsync();
        var mappedCourses = mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseForListDto>>(courses);
        return Result<IReadOnlyList<CourseForListDto>>.Success(mappedCourses);
    }

    public async Task<Result<Pagination<CourseDto>>> Handle(
        GetCoursesWithTopicsQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new GetCoursesWithTopicsSpecification(request.Parameters);
        var courses = await unitOfWork.Repository<Course>()!.GetAllWithSpecificationAsync(specification);
        var mappedCourses = mapper.Map<IReadOnlyList<CourseDto>>(courses);
        var countSpecification = new CountCoursesWithFilterationSpecification(request.Parameters);
        return Result<Pagination<CourseDto>>.Success(new(
            request.Parameters.PageNumber,
            request.Parameters.PageSize,
            await unitOfWork.Repository<Course>()!.CountWithSpecificationAsync(countSpecification),
            mappedCourses));
    }
}
