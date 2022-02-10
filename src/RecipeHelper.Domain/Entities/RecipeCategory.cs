namespace RecipeHelper.Domain.Entities
{
    public class RecipeCategory
    {
        public Guid RecipeId { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual Category Category { get; set; }
    }
}
