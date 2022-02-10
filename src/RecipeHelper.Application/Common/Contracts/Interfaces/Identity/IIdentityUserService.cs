using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.Requests.Users;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Identity
{
    public interface IIdentityUserService
    {
        Task<Response<ApplicationUserDto>> GetUserByIdAsync(string id);
        Task<Response<PaginatedList<ApplicationUserDto>>> GetPaginatedUsersAsync(int? pageNumber, int? pageSize);
        Task<Response<ApplicationUserDto>> CreateUserAsync(CreateUserRequest request);
        Task<Response<ApplicationUserDto>> UpdateUserAsync(UpdateUserRequest request);
        Task<Response<ApplicationUserDto>> DeleteUserAsync(string id);
        Task<Response<ApplicationUserDto>> ManageUserRoleAsync(ManageUserRoleRequest request);
    }
}
