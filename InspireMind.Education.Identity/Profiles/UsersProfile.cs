using AutoMapper;
using InspireMind.Education.Application.Features.Users.DTOs;
using InspireMind.Education.Identity.Entities;

namespace InspireMind.Education.Identity.Profiles;
public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<AppUser, UserListDto>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => string.Concat(src.FirstName, " ", src.LastName)));

        CreateMap<UserForUpdateDto, AppUser>();
    }
}
