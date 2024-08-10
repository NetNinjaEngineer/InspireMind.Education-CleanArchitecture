using AutoMapper;
using InspireMind.Education.Application.DTOs.Topic;
using InspireMind.Education.Domain.Entities;

namespace EduConnect.Application.Profiles;
public class TopicsProfile : Profile
{
    public TopicsProfile()
    {
        CreateMap<Topic, TopicDto>();
        CreateMap<TopicForCreationDto, Topic>();
        CreateMap<TopicForUpdateDto, Topic>();
    }
}
