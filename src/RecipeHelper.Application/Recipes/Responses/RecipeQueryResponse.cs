using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Dtos;

namespace RecipeHelper.Application.Recipes.Responses
{
    public class RecipeQueryResponse : IResponse<RecipeQueryDto>
    {
        public RecipeQueryDto Data { get; set; }
    }
}
