using RecipeHelper.Application.Common.Base;
using RecipeHelper.Domain.Base;
using RecipeHelper.Domain.Enum;

namespace RecipeHelper.Domain.Entities
{
    public class Recipe : AuditableEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
