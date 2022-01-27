using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Contracts.Persistance;
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
            //var validator = new UpdateIngredientValidator(_repository);

            //var result = await validator.ValidateAsync(request, cancellationToken);

            //if (result.Errors.Count > 0)
            //    return Response<IngredientDto>.Fail("Validation didnt pass.", JsonSerializer.Serialize(result.Errors).Split(",").ToList());

            var ingredient = await _repository.GetByIdAsync(request.Id);

            if (ingredient == null)
                return Response<IngredientDto>.Fail("Ingredient is null");

            try
            {   
                ingredient.Name = request.Name;
                ingredient.LastmodifiedBy = _userService.GetUser()?.ToString() ?? "God";
                ingredient.LastModifiedDate = DateTime.UtcNow;

                await _repository.UpdateAync(ingredient);
                _logger.LogInformation($"New ingredient: {request.Name} Created by {_userService.GetUser()?.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return Response<IngredientDto>.Success("New ingredient created");

        }
    }
}
