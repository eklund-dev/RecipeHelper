using RecipeHelper.Application.Common.Requests.Users;
using RecipeHelper.Application.Common.Responses.Identity;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Identity
{
    public interface IIdentityUserService
    {
        Task<ApplicationUserResponse> GetByIdAsync(string id);
        Task<IReadOnlyList<ApplicationUserResponse>> GetAllAsync();
        Task<ApplicationUserResponse> CreateAsync(CreateUserRequest request);
        Task<ApplicationUserResponse> UpdateAsync(UpdateUserRequest request);
        Task<ApplicationUserResponse> DeleteAsync(string id);
        Task<ApplicationUserResponse> ManageUserRoleAsync(ManageUserRoleRequest request);
    }
}
