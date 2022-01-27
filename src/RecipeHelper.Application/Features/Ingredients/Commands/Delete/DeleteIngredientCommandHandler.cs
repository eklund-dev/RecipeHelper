using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.Ingredients.Commands.Delete
{
    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, Response<IngredientDto>>
    {
        private readonly IAsyncRepository<Ingredient, Guid> _repository;
        private readonly ILogger<DeleteIngredientCommandHandler> _logger;

        public DeleteIngredientCommandHandler(IAsyncRepository<Ingredient, Guid> repository, ILogger<DeleteIngredientCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Response<IngredientDto>> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _repository.GetByIdAsync(request.Id);

            if (ingredient == null)
                return Response<IngredientDto>.Fail($"Ingredient with id: {request.Id} could not be found");

            try
            {
                await _repository.DeleteAsync(ingredient);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when deleting ingredient", ex.Message);
                throw;
            }

            return Response<IngredientDto>.Success($"Ingredient: {ingredient.Name} with id {ingredient.Id} successfully deleted");
        }
    }
}
