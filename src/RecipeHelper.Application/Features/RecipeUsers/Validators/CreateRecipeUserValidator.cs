using FluentValidation;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Validators;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.RecipeUsers.Validators
{
    public class CreateRecipeUserValidator : AbstractValidator<RecipeUser>
    {
        private readonly IRecipeUserRepository _repository;
        public CreateRecipeUserValidator(IRecipeUserRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .MaximumLength(15);
        }

        //private async Task<bool> VerifyUniqueName()
        //{

        //}
    }
}
