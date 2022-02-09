using AutoMapper;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
        }
    }
}
