using FluentValidation;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Validators;
using RecipeHelper.Application.Features.Recipes.Commands.Create;
using RecipeHelper.Domain.Enum;

namespace RecipeHelper.Application.Features.Recipes.Validators
{
    public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
    {
        private readonly IRecipeRepository _repository;
        private readonly IFoodTypeRepository _foodTypeRepository;

        public CreateRecipeCommandValidator(IRecipeRepository repository, IFoodTypeRepository foodTypeRepository)
        {
            _repository = repository;
            _foodTypeRepository = foodTypeRepository;

            RuleFor(x => x.Recipe.Name)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .NotNull().WithMessage("{PropertyName} cant no be null")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .MaximumLength(50).WithMessage("Max 25 characeters");

            RuleFor(x => x.Recipe.Description)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .NotNull().WithMessage("{PropertyName} cant no be null")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .MaximumLength(300).WithMessage("Max 300 characeters");

            RuleForEach(x => x.Recipe.Instructions)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .NotNull().WithMessage("{PropertyName} cant no be null")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace();

            RuleFor(x => x.Recipe.FoodTypeId)
                .NotEmpty().WithMessage("{PropertyName} can not be empty");


            RuleFor(e => e)
                .MustAsync(FoodTypeMustExist).WithMessage(x => $"{x.Recipe.FoodTypeId} does not exist");

            RuleFor(x => x.Recipe.Difficulty).IsEnumName(typeof(Difficulty), caseSensitive: false);
            RuleFor(x => x.Recipe.TypeOfOccasion).IsEnumName(typeof(TypeOfOccasion), caseSensitive: false);
            RuleFor(x => x.Recipe.TypeOfMeal).IsEnumName(typeof(TypeOfMeal), caseSensitive: false);

            RuleFor(e => e)
                .MustAsync(RecipeNameUnique).WithMessage(x => $"{x.Recipe.Name} already exists - try another one");

        }
        private async Task<bool> RecipeNameUnique(CreateRecipeCommand cmd, CancellationToken token)
        {
            return !await _repository.IsRecipeNameUnique(cmd.Recipe.Name, token);
        }

        private async Task<bool> FoodTypeMustExist(CreateRecipeCommand cmd, CancellationToken token)
        {
            return await _foodTypeRepository.FoodTypeExistsAsync(cmd.Recipe.FoodTypeId);
        }

    }
}
