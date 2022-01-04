using RecipeHelper.Domain.Base;
using RecipeHelper.Domain.Enum;

namespace RecipeHelper.Domain.Entities
{
    public class Recipe : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public virtual Difficulty Difficulty { get; set; }
        public virtual Duration Duration { get; set; }
        public virtual RecipeOccassion Occasion { get; set; }


        public int? CourseCategoryId { get; set; }
        public virtual CourseCategory? CourseCategory { get; set; }

        public int? RecipeFoodTypeId { get; set; }
        public virtual RecipeFoodType? RecipeFoodType { get; set; }

        public virtual ICollection<Ingredient>? Ingredients { get; set; }
    }
}
