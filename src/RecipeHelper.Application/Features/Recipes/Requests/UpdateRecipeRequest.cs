using RecipeHelper.Application.Common.Dtos;

namespace RecipeHelper.Application.Features.Recipes.Requests
{
    public class UpdateRecipeRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Difficulty { get; set; }
        public string TypeOfOccasion { get; set; }
        public string TypeOfMeal { get; set; }
        public Guid FoodTypeId { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<string> Instructions { get; set; }
        public List<UpdateRecipeIngredientDto> RecipeIngredients { get; set; }
    }
}
