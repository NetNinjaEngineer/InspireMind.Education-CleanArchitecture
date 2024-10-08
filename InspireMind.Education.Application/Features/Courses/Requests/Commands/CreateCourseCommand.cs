﻿using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.Features.Courses.DTOs;
using MediatR;

namespace InspireMind.Education.Application.Features.Courses.Requests.Commands;
public sealed class CreateCourseCommand : IRequest<Result<CourseForListDto>>
{
    public CourseForCreateDto Course { get; set; } = null!;
}
