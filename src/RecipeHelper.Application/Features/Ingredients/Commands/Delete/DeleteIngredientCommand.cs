using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Ingredients.Commands.Delete
{
    public class DeleteIngredientCommand : IRequest<Response<IngredientDto>>
    {
        public Guid Id { get; set; }  
    }
}
