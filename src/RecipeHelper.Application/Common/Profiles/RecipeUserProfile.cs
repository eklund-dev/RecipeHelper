using AutoMapper;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class RecipeUserProfile : Profile
    {
        public RecipeUserProfile()
        {
            CreateMap<RecipeUser, RecipeUserDto>()
                .ForMember(x => x.Recipes, opt => opt.MapFrom(src => src.FavoriteRecipes.Select(y => y.Recipe)));
        }
    }
}
