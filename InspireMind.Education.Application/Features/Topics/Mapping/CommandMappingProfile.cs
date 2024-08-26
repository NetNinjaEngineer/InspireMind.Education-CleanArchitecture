using AutoMapper;
using InspireMind.Education.Application.Features.Topics.DTOs;
using InspireMind.Education.Domain.Entities;

namespace InspireMind.Education.Application.Features.Topics.Mapping;
internal class CommandMappingProfile : Profile
{
    public CommandMappingProfile()
    {
        CreateMap<TopicForCreationDto, Topic>();
        CreateMap<TopicForUpdateDto, Topic>();
    }
}
