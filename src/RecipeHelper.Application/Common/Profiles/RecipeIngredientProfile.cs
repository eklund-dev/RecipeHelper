using AutoMapper;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class RecipeIngredientProfile : Profile
    {
        public RecipeIngredientProfile()
        {
            CreateMap<RecipeIngredient, RecipeIngredientDto>()
                .ForMember(x => x.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(x => x.Portions, opt => opt.MapFrom(src => src.NumberOfPortionsBase))
                .ForMember(x => x.Amount, opt => opt.MapFrom(src => src.IngredientAmountBase))
                .ReverseMap();

            CreateMap<CreateRecipeIngredientDto, RecipeIngredient>()
                .ForMember(x => x.NumberOfPortionsBase, opt => opt.MapFrom(src => src.Portions))
                .ForMember(x => x.IngredientAmountBase, opt => opt.MapFrom(src => src.Amount));
        }
    }
}
