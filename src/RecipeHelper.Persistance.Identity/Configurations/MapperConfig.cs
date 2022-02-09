using AutoMapper;
using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Persistance.Identity.Dtos;
using RecipeHelper.Persistance.Identity.Models;

namespace RecipeHelper.Persistance.Identity.Configurations
{
    public static class MapperConfig
    {
        public static MapperConfiguration GetUserWithRolesConfig()
        {
            return new MapperConfiguration(config =>
                config.CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(innerDto => innerDto.Roles,
                    config => config.MapFrom(user => user.UserRoles!.Select(x => x.Role.Name))));
        }

        public static MapperConfiguration GetRoleWithUsersConfig()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ApplicationUser, UserInRoleDto>()
                   .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.Id));

                config.CreateMap<ApplicationRole, ApplicationRoleDto>()
                        .ForMember(dto => dto.Users, 
                        config => config.MapFrom(src => src.UserRoles.Select(x => x.User)));
            });
        }
    }
}
