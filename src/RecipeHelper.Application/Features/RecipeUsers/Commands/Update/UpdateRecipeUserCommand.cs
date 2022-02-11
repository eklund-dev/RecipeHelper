using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.RecipeUsers.Commands.Delete
{
    public class UpdateRecipeUserCommand : IRequest<Response<RecipeUserDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Guid> RecipeIds { get; set; }
    }
}
