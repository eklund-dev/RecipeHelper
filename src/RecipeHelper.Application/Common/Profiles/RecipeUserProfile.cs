using AutoMapper;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class RecipeUserProfile : Profile
    {
        public RecipeUserProfile()
        {
            CreateMap<RecipeUser, RecipeUserDto>();

            CreateMap<FavoriteRecipe, RecipeDto>().ReverseMap();
        }
    }
}
