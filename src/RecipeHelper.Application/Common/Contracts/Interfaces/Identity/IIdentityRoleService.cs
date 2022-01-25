using RecipeHelper.Application.Common.Requests.Roles;
using RecipeHelper.Application.Common.Responses.Identity;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Identity.Roles
{
    public interface IIdentityRoleService
    {
        Task<ApplicationRoleResponse> GetByIdAsync(string id);
        Task<IReadOnlyList<ApplicationRoleResponse>> GetAllAsync();
        Task<ApplicationRoleResponse> CreateAsync(CreateRoleRequest request);
        Task<ApplicationRoleResponse> UpdateAsync(UpdateRoleRequest request);
        Task<ApplicationRoleResponse> DeleteAsync(string id);
    }
}
