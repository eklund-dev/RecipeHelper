using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Common.Helpers;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Enum;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Recipes.Commands.Create
{
    public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, Response<RecipeDto>>
    {
        private readonly IRecipeRepository _repository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly IRecipeIngredientRepository _ingredientRepository;
        private readonly ILogger<CreateRecipeCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;
        private readonly IMapper _mapper;

        public CreateRecipeCommandHandler(
            IRecipeRepository repository,
            IFoodTypeRepository foodTypeRepository,
            IRecipeIngredientRepository ingredientRepository,
            ILogger<CreateRecipeCommandHandler> logger,
            IHttpContextUserService userService,
            IMapper mapper) 
        {
            _repository = repository;
            _foodTypeRepository = foodTypeRepository;
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
        }
        public async Task<Response<RecipeDto>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string userName = _userService.GetClaimsUserName();

                var newRecipe = _mapper.Map<Recipe>(request.Recipe);
                newRecipe.Id = Guid.NewGuid();
                newRecipe.Name = StringHelpers.ToTitleCase(newRecipe.Name);
                newRecipe.CreatedBy = userName;
                newRecipe.CreatedDate = DateTime.UtcNow;

                if (newRecipe.RecipeIngredients != null)
                {
                    foreach (var ir in newRecipe.RecipeIngredients)
                    {
                        ir.RecipeId = newRecipe.Id;
                    }
                }      

                var recipeCategories = new List<RecipeCategory>();

                foreach (var category in request.Recipe.Categories)
                {
                    recipeCategories.Add(new RecipeCategory { RecipeId = newRecipe.Id, CategoryId = category.Id });
                }


                newRecipe.RecipeCategories = recipeCategories;

                var addedRecipe = await _repository.AddAsync(newRecipe);
                _logger.LogInformation($"New recipe added");

                return Response<RecipeDto>.Success(_mapper.Map<RecipeDto>(addedRecipe), $"Recipe added successfully by {userName}");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {nameof(CreateRecipeCommandHandler)}", ex.Message);
                throw new ApiException($"Exception occured in {nameof(CreateRecipeCommandHandler)}", ex.InnerException.Message);
            }
           
        }

    }
}
