using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Recipes.Commands.Delete
{
    public class DeleteRecipeCommand : IRequest<Response<RecipeDto>>
    {
        public Guid Id { get; set; }
    }

}
