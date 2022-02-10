using AutoMapper;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class FoodTypeProfile : Profile
    {
        public FoodTypeProfile()
        {
            CreateMap<FoodType, FoodTypeDto>().ReverseMap();
        }
    }
}
