using FluentValidation;
using RecipeHelper.Application.Common.Requests.Auth;

namespace RecipeHelper.Application.Common.Validators.Auth
{
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .EmailAddress().WithMessage("{PropertyName} is Not a valid E-mailaddress");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$")
                    .WithMessage(
                    "Password Criteria not met \n" +
                    "At least one upper case english letter \n" +
                    "At least one lower case english letter +\n" +
                    "At least one digit \n" +
                    "At least one special character \n" +
                    "Minimum 6 in length");

        }
    }
}
