using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Recipes.Requests;

namespace RecipeHelper.Application.Features.Recipes.Commands.Create
{
    public class CreateRecipeCommand : IRequest<Response<RecipeDto>>
    {
        public CreateRecipeRequest Recipe { get; set; }
    }
}
