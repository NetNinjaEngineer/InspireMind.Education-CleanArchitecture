using AutoMapper;
using InspireMind.Education.Application.Features.Courses.DTOs;
using InspireMind.Education.Domain.Entities;

namespace InspireMind.Education.Application.Features.Courses.Mapping;
internal class CommandMappingProfile : Profile
{
    public CommandMappingProfile()
    {
        CreateMap<CourseForUpdateDto, Course>();
        CreateMap<CourseForCreateDto, Course>();
    }
}
