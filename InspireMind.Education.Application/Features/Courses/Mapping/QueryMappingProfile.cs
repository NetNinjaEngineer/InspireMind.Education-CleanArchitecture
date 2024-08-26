using AutoMapper;
using InspireMind.Education.Application.Features.Courses.DTOs;
using InspireMind.Education.Domain.Entities;

namespace InspireMind.Education.Application.Features.Courses.Mapping;
internal class QueryMappingProfile : Profile
{
    public QueryMappingProfile()
    {
        CreateMap<Course, CourseForListDto>();

        CreateMap<Course, CourseDto>()
         .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.Topic!.TopicName));
    }
}
