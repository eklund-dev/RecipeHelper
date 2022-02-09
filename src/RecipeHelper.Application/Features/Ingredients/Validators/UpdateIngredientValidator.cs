using FluentValidation;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Validators;
using RecipeHelper.Application.Features.Ingredients.Commands.Update;

namespace RecipeHelper.Application.Features.Ingredients.Validators
{
    public class UpdateIngredientValidator : AbstractValidator<UpdateIngredientCommand>
    {
        private readonly IIngredientRepository _repository;

        public UpdateIngredientValidator(IIngredientRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Can not be empty.")
                .NotNull().WithMessage("Can not be null.")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .MaximumLength(25).WithMessage("'{PropertyValue}' can have a 'Max' character length of 25.");

            RuleFor(e => e.Name)
                .MustAsync(IngredientNameUnique).WithMessage("'{PropertyValue}' already exists - try another one.");
        }

        private async Task<bool> IngredientNameUnique(string name, CancellationToken token)
        {
            return !await _repository.IsIngredientNameUnique(name, token);
        }
    }
}
