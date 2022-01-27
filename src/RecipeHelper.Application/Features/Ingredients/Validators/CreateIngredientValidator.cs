using FluentValidation;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Validators;
using RecipeHelper.Application.Features.Ingredients.Commands.Create;

namespace RecipeHelper.Application.Features.Ingredients.Validators
{
    public class CreateIngredientValidator : AbstractValidator<CreateIngredientCommand>
    {
        private readonly IIngredientRepository _repository;

        public CreateIngredientValidator(IIngredientRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} can not be empty.")
                .NotNull().WithMessage("{PropertyName} can not be null.")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .MaximumLength(25).WithMessage("{PropertyName} can have a Maximum character length of 25");

            RuleFor(expression => expression)
                .MustAsync(IngredientNameUnique).WithMessage("{PropertyName} already exists - try another one");
        }

        private async Task<bool> IngredientNameUnique(CreateIngredientCommand cmd, CancellationToken token)
        {
            return !await _repository.IsIngredientNameUnique(cmd.Name);
        }
    }
}
