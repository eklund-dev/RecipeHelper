using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class FoodType : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Recipe>? Recipes { get; set; }
    }
}
