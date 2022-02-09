using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Categories.Commands.Create
{
    public class DeleteCategoryCommand : IRequest<Response<CategoryDto>>
    {
        public Guid Id { get; set; }
    }
}
