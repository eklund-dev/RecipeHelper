using FluentValidation;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Validators;
using RecipeHelper.Application.Features.Recipes.Commands.Create;
using RecipeHelper.Application.Features.Recipes.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHelper.Application.Features.Recipes.Validators
{
    public class UpdateRecipeCommandValidator : AbstractValidator<UpdateRecipeCommand>
    {
        private readonly IRecipeRepository _repository;
        private readonly IFoodTypeRepository _foodTypeRepository;

        public UpdateRecipeCommandValidator(IRecipeRepository repository, IFoodTypeRepository foodTypeRepository)
        {
            _repository = repository;
            _foodTypeRepository = foodTypeRepository;

            RuleFor(x => x.Recipe.Name)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .NotNull().WithMessage("{PropertyName} cant no be null")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .MaximumLength(50).WithMessage("Max 50 characeters")
                .MustAsync(RecipeNameUnique).WithMessage("'{PropertyValue}' must be unique");

            RuleFor(x => x.Recipe.Description)
                .NotEmpty().WithMessage("Can not be empty")
                .NotNull().WithMessage("Can not be null")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .MaximumLength(300).WithMessage("Max 300 characeters");

            RuleForEach(x => x.Recipe.Instructions)
                .NotEmpty().WithMessage("Can not be empty")
                .NotNull().WithMessage("Cant no be null")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace();

            RuleFor(x => x.Recipe.FoodTypeId)
                .NotEmpty().WithMessage("Can not be empty")
                .NotNull().WithMessage("Can not be null")
                .MustAsync(FoodTypeMustExist).WithMessage("'{PropertyValue}' does not exist");
        }
        private async Task<bool> RecipeNameUnique(string recipeName, CancellationToken token)
        {
            return !await _repository.IsRecipeNameUnique(recipeName, token);
        }

        private async Task<bool> FoodTypeMustExist(Guid foodTypeId, CancellationToken token)
        {
            return await _foodTypeRepository.FoodTypeExistsAsync(foodTypeId);
        }
    }
}
