using AutoMapper;
using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Features.Identity.Requests.Roles;
using RecipeHelper.Application.Features.Identity.Requests.Users;
using RecipeHelper.Persistance.Identity.Dtos;
using RecipeHelper.Persistance.Identity.Models;

namespace RecipeHelper.Persistance.Identity.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<CreateUserRequest, ApplicationUser>();
            
            CreateMap<ApplicationUser, ApplicationUserDto>()
                 .ForMember(dest => dest.Roles,
                    options => options.MapFrom(src => src.UserRoles!.Select(x => x.Role.Name)));

            CreateMap<ApplicationRole, ApplicationRoleDto>();

            CreateMap<ApplicationUser, UserInRoleDto>();

            CreateMap<UpdateRoleRequest, ApplicationRole>();
        }
    }
}
