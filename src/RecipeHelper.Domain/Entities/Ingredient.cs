using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class Ingredient : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }   
    }
}
