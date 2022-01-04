using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class RecipeUser : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid IdentityId { get; set; }
        public virtual ICollection<FavoriteRecipe>? FavoriteRecipes { get; set; }
    }
}
