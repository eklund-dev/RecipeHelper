using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.RecipeUsers.Commands.Delete
{
    public class DeleteRecipeUserCommand : IRequest<Response<RecipeUserDto>>
    {
        public Guid Id { get; set; }
    }
}
