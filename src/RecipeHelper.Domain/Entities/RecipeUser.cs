using RecipeHelper.Application.Common.Base;
using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class RecipeUser : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid IdentityId { get; set; }
        public virtual ICollection<Recipe>? FavoriteRecipes { get; set; }
    }
}
