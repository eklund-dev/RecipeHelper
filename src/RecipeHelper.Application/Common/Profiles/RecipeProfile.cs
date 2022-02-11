using AutoMapper;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Features.Recipes.Requests;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>()
                .ForMember(x => x.Categories, opt => opt.MapFrom(src => src.RecipeCategories.Select(y => y.Category)))
                .ForMember(x => x.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients));

            CreateMap<UpdateRecipeRequest, Recipe>();

            CreateMap<CreateRecipeRequest, Recipe>();

        }
    }
}
