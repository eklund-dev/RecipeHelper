using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Ingredients.Commands.Delete
{
    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, Response<IngredientDto>>
    {
        private readonly IAsyncRepository<Ingredient, Guid> _repository;
        private readonly ILogger<DeleteIngredientCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;

        public DeleteIngredientCommandHandler(IAsyncRepository<Ingredient, Guid> repository, ILogger<DeleteIngredientCommandHandler> logger, IHttpContextUserService userService)
        {
            _repository = repository;
            _logger = logger;
            _userService = userService;
        }
        public async Task<Response<IngredientDto>> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _repository.GetByIdAsync(request.Id);
            var userName = _userService.GetClaimsUserName();

            if (ingredient == null)
                return Response<IngredientDto>.Fail($"Ingredient with id: {request.Id} could not be found");

            try
            {
                await _repository.DeleteAsync(ingredient);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when deleting ingredient by {userName}", ex.Message);
                throw new ApiException($"Error when deleting ingredient by {userName}", ex.Message);
            }

            return Response<IngredientDto>.Success($"Ingredient: {ingredient.Name} with id {ingredient.Id} successfully deleted by {userName}");
        }
    }
}
