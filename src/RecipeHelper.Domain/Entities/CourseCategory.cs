using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class CourseCategory : AuditableEntity, IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Recipe>? Recipes { get; set; }

    }
}
