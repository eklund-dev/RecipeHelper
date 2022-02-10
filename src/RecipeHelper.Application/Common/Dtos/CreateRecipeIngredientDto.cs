namespace RecipeHelper.Application.Common.Dtos
{
    public class CreateRecipeIngredientDto
    {
        public Guid IngredientId { get; set; }
        public int Portions { get; set; }
        public decimal Amount { get; set; }
    }
}
