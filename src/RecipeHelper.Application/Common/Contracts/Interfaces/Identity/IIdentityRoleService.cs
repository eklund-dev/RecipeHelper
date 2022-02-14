using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Identity.Requests.Roles;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Identity.Roles
{
    public interface IIdentityRoleService
    {
        Task<Response<ApplicationRoleDto>> GetByIdAsync(string id);
        Task<Response<PaginatedList<ApplicationRoleDto>>> GetAllAsync(int? pageNumber, int? pageSize);
        Task<Response<ApplicationRoleDto>> CreateAsync(CreateRoleRequest request);
        Task<Response<ApplicationRoleDto>> UpdateAsync(UpdateRoleRequest request);
        Task<Response<ApplicationRoleDto>> DeleteAsync(string id);
    }
}
