using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Ingredients.Commands.Update
{
    public class UpdateIngredientCommand : IRequest<Response<IngredientDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
