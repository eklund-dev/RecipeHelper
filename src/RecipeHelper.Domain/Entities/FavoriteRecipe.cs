using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class FavoriteRecipe : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get ; set; } = null!;
        public Guid RecipeUserId { get; set; }
        public virtual RecipeUser? RecipeUser { get; set; }
    }
}
