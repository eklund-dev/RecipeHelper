using AutoMapper;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Features.Recipes.Requests;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Enum;

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
                //.ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => Enum.GetName(typeof(Difficulty), src.Difficulty)))
                //.ForMember(dest => dest.TypeOfMeal, opt => opt.MapFrom(src => Enum.GetName(typeof(TypeOfMeal), src.TypeOfMeal)))
                //.ForMember(dest => dest.TypeOfOccasion, opt => opt.MapFrom(src => Enum.GetName(typeof(TypeOfOccasion), src.TypeOfOccasion)));

            CreateMap<CreateRecipeRequest, Recipe>();
        //        .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => Enum.GetName(typeof(Difficulty), src.Difficulty)))
        //        .ForMember(dest => dest.TypeOfMeal, opt => opt.MapFrom(src => Enum.GetName(typeof(TypeOfMeal), src.TypeOfMeal)))
        //        .ForMember(dest => dest.TypeOfOccasion, opt => opt.MapFrom(src => Enum.GetName(typeof(TypeOfOccasion), src.TypeOfOccasion)));
        }
    }
}
