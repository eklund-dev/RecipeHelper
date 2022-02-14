using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Recipes.Commands.Update
{
    public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, Response<RecipeDto>>
    {
        private readonly IRecipeRepository _repository;
        private readonly IRecipeIngredientRepository _ingredientRepository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRecipeCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;

        public UpdateRecipeCommandHandler(
            IRecipeRepository repository,
            IMapper mapper,
            ILogger<UpdateRecipeCommandHandler> logger,
            IHttpContextUserService userService,
            IRecipeIngredientRepository ingredientRepository, 
            IFoodTypeRepository foodTypeRepository,
            ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
            _ingredientRepository = ingredientRepository;
            _foodTypeRepository = foodTypeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<RecipeDto>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _repository.GetByIdAsync(request.Recipe.Id);
            // Denna ska validatorn klara av
            if (recipe == null)
            {
                return Response<RecipeDto>.Fail($"Recipe with id {request.Recipe.Id} does not exist in the database");
            }

            try
            {
                // TODO: snygga till denna klass - bryt ut allt som går att bryta ut.
                var recipeCategories = new List<RecipeCategory>();

                foreach (var category in request.Recipe.Categories)
                {
                    recipeCategories.Add(new RecipeCategory { RecipeId = recipe.Id, CategoryId = category.Id });
                }

                var recipeIngredients = new List<RecipeIngredient>();

                foreach (var ri in request.Recipe.RecipeIngredients)
                {
                    recipeIngredients.Add(new RecipeIngredient 
                    { 
                        RecipeId = request.Recipe.Id,
                        IngredientId = ri.IngredientId,
                        IngredientAmountBase = ri.Amount,
                        NumberOfPortionsBase = ri.Portions
                    });
                }

                var userName = _userService.GetClaimsUserName();

                var recipeToUpdate = _mapper.Map<Recipe>(request.Recipe);

                recipeToUpdate.RecipeCategories = recipeCategories;
                recipeToUpdate.RecipeIngredients = recipeIngredients;

                recipeToUpdate.CreatedDate = recipe.CreatedDate;
                recipeToUpdate.CreatedBy = recipe.CreatedBy;
                recipeToUpdate.LastModifiedDate = DateTime.UtcNow;
                recipeToUpdate.LastmodifiedBy = userName;

                await _repository.UpdateAync(recipeToUpdate);

                var recipeDto = _repository.Entity.ProjectTo<RecipeDto>(_mapper.ConfigurationProvider).FirstOrDefault();

                if (recipeDto is null)
                {
                    return Response<RecipeDto>.Fail("Failed mapping Recipe Entity to RecipeDto");
                }

                _logger.LogInformation($"Recipe with id {request.Recipe.Id} has been updated by {userName}");

                return Response<RecipeDto>.Success("Recipe updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in {nameof(UpdateRecipeCommandHandler)}", ex.Message);
                throw new ApiException("Updating Recipe failed", ex.Message);
            }
        }
    }
}
