using FluentValidation;
using RecipeHelper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHelper.Application.Features.RecipeUsers.Validators
{
    public class CreateRecipeUserValidator : AbstractValidator<RecipeUser>
    {
        public CreateRecipeUserValidator()
        {

        }
    }
}
