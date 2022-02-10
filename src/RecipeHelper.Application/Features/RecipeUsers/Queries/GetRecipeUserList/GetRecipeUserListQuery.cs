using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.RecipeUsers.Queries.GetRecipeUserList
{
    public class GetRecipeUserListQuery : IRequest<Response<PaginatedList<RecipeUserDto>>>
    {
        public QueryParameters QueryParameters { get; set; }
    }
}
