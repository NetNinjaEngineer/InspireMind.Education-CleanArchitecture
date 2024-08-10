﻿using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.DTOs.Course;
using MediatR;

namespace InspireMind.Education.Application.Features.Courses.Requests.Queries;

public class GetCoursesListQuery : IRequest<Result<IReadOnlyList<CourseForListDto>>>
{

}