using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class FavoriteRecipe : AuditableEntity
    {
        public Guid RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public Guid RecipeUserId { get; set; }
        public virtual RecipeUser? RecipeUser { get; set; }
    }
}
