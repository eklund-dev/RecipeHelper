using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.RecipeUsers.Commands.Create
{
    public class CreateRecipeUserCommand : IRequest<Response<RecipeUserDto>>
    {
        public string Name { get; set; }
    }
}
