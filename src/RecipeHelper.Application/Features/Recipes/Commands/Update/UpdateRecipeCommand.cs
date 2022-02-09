using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Recipes.Requests;

namespace RecipeHelper.Application.Features.Recipes.Commands.Update
{
    public class UpdateRecipeCommand : IRequest<Response<RecipeDto>>
    {
        public UpdateRecipeRequest Recipe { get; set; }
    }
}
