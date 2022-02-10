using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.RecipeUsers.Queries.GetRecipeUserDetails
{
    public class GetRecipeUserDetailsByIdQuery : IRequest<Response<RecipeUserDto>>
    {
        public Guid Id { get; set; }
    }
}
