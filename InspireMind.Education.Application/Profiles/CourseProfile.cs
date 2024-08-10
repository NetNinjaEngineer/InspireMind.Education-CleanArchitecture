using AutoMapper;
using InspireMind.Education.Application.DTOs.Course;
using InspireMind.Education.Domain.Entities;

namespace EduConnect.Application.Profiles;
internal class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.Topic!.TopicName));

        CreateMap<CourseForUpdateDto, Course>();
        CreateMap<CourseForCreateDto, Course>();
        CreateMap<Course, CourseForListDto>();
    }
}
