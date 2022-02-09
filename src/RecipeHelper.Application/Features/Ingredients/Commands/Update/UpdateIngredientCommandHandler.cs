using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Ingredients.Commands.Update;
using RecipeHelper.Application.Features.Ingredients.Validators;
using System.Text.Json;

namespace RecipeHelper.Application.Features.Ingredients.Commands.Create
{
    public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, Response<IngredientDto>>
    {
        private readonly IIngredientRepository _repository;
        private readonly ILogger<UpdateIngredientCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;

        public UpdateIngredientCommandHandler(
            IIngredientRepository repository,
            IMapper mapper,
            ILogger<UpdateIngredientCommandHandler> logger, IHttpContextUserService userService)
        {
            _repository = repository;
            _logger = logger;
            _userService = userService;
        }
        public async Task<Response<IngredientDto>> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _repository.GetByIdAsync(request.Id);

            if (ingredient == null)
                return Response<IngredientDto>.Fail("Ingredient is null");

            var userName = _userService.GetUser()?.Identity?.Name?.ToString();

            try
            {
                ingredient.Name = request.Name;
                ingredient.LastmodifiedBy = userName ?? "God";
                ingredient.LastModifiedDate = DateTime.UtcNow;

                await _repository.UpdateAync(ingredient);
                _logger.LogInformation($"New ingredient: {request.Name} Created by {userName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return Response<IngredientDto>.Success($"New ingredient created by {userName ?? "God"}.");

        }
    }
}
