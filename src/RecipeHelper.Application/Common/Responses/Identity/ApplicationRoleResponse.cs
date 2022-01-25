using RecipeHelper.Persistance.Identity.Dtos;

namespace RecipeHelper.Application.Common.Responses.Identity
{
    public class ApplicationRoleResponse : BaseResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserInRoleDto>? Users { get; set; }
    }
}
