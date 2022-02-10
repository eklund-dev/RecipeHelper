using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class Category : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeCategory>? RecipeCategories { get; set; }
    }
}
