using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryByIdQuery : IRequest<Response<CategoryDto>>
    {
        public Guid Id { get; set; }
    }
}
