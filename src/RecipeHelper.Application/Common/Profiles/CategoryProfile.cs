using AutoMapper;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Features.Categories.Commands.Create;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<UpdateCategoryCommand, Category>();

        }
    }
}
