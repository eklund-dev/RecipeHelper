using RecipeHelper.Domain.Base;
using RecipeHelper.Domain.Enum;

namespace RecipeHelper.Domain.Entities
{
    public class Recipe : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<string> Instructions { get; set; }
        public TimeSpan Duration { get; set; }

        public virtual Difficulty Difficulty { get; set; }
        public virtual TypeOfOccasion TypeOfOccasion { get; set; }
        public virtual TypeOfMeal TypeOfMeal { get; set; }

        public Guid? FoodTypeId { get; set; }
        public virtual FoodType? FoodType { get; set; }
        
        public virtual ICollection<RecipeCategory>? RecipeCategories { get; set; }
        public virtual ICollection<FavoriteRecipe>? FavoriteRecipes { get; set; }
        public virtual ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}
