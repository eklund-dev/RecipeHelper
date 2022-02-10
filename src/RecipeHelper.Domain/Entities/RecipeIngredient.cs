using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class RecipeIngredient
    {
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public Guid IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }  

        public int NumberOfPortionsBase { get; set; }     
        public decimal IngredientAmountBase { get; set; }
        public decimal IngredientAmount => IngredientAmountBase * NumberOfPortionsBase;
    }
}
