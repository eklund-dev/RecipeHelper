using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Recipes.Responses
{
    public class RecipeQueryResponse : BaseResponse, ISingleEntityResponse<RecipeQueryDto>
    {
        public RecipeQueryDto Data { get; set; }
    }
}
