using RecipeHelper.Persistance.Identity.Dtos;

namespace RecipeHelper.Application.Common.Dtos.Identity
{
    public class ApplicationRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserInRoleDto>? Users { get; set; }
    }
}
