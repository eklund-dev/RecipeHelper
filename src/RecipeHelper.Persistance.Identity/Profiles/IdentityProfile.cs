using AutoMapper;
using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.Requests.Users;
using RecipeHelper.Persistance.Identity.Models;

namespace RecipeHelper.Persistance.Identity.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<CreateUserRequest, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();

            CreateMap<ApplicationRole, ApplicationRoleDto>();
        }
    }
}
