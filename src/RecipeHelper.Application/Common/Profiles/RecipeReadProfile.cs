using AutoMapper;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class RecipeReadProfile : Profile
    {
        public RecipeReadProfile()
        {
            CreateMap<Recipe, RecipeQueryDto>();
        }
    }
}
