using RecipeHelper.Persistance.Identity.Dtos;

namespace RecipeHelper.Application.Features.Identity.Responses
{
    public class ApplicationRoleResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserInRoleDto>? Users { get; set; }
    }
}
