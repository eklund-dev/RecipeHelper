using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Ingredients.Commands.Create
{
    public class CreateIngredientCommand : IRequest<Response<IngredientDto>>
    {
        public string Name { get; set; }
    }
}
