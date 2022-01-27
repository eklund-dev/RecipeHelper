using AutoMapper;
using RecipeHelper.Application.Features.Recipes;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>();
        }
    }
}
