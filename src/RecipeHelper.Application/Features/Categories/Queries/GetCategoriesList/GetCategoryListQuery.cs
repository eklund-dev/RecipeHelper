using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoryListQuery : IRequest<Response<PaginatedList<CategoryDto>>>
    {
        public QueryParameters QueryParameters { get; set; }
    }
}
