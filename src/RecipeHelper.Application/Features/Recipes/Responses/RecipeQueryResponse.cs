using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Recipes.Responses
{
    public class RecipeQueryAllResponse : BaseResponse, IMultipleEntitiesResponse<RecipeQueryDto>
    {
        public IEnumerable<RecipeQueryDto> Data { get; set; }
    }
}
