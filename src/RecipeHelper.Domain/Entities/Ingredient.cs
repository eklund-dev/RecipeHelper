using RecipeHelper.Application.Common.Base;
using RecipeHelper.Domain.Base;

namespace RecipeHelper.Domain.Entities
{
    public class Ingredient : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Recipe>? Recipes { get; set; }
       
    }
}
