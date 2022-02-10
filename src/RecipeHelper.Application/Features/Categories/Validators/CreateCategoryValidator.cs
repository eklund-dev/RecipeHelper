using FluentValidation;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Validators;
using RecipeHelper.Application.Features.Categories.Commands.Create;

namespace RecipeHelper.Application.Features.Categories.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        public CreateCategoryValidator(ICategoryRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Can not be empty.")
                .NotNull().WithMessage("Can not be null.")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .MaximumLength(25).WithMessage("'{PropertyValue}' can have a Maximum character length of 25");

            RuleFor(e => e.Name)
                .MustAsync(IsNameUnique).WithMessage("'{PropertyValue}' already exists - try another one");
                    
        }
        private async Task<bool> IsNameUnique(string name, CancellationToken token)
        {
            return !await _repository.IsCategoryNameUnique(name, token);
        }
    }
}
