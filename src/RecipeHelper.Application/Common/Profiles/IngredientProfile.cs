using AutoMapper;
using RecipeHelper.Application.Features.Ingredients;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>();
        }
    }
}
