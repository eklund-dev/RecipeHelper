using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity.Roles;
using RecipeHelper.Application.Common.Requests.Roles;
using RecipeHelper.Application.Common.Responses.Identity;
using RecipeHelper.Persistance.Identity.Context;
using RecipeHelper.Persistance.Identity.Dtos;
using RecipeHelper.Persistance.Identity.Models;

namespace RecipeHelper.Persistance.Identity.Services
{
    public class IdentityRoleService : IIdentityRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<IdentityRoleService> _logger;
        private readonly IHttpContextUserService _userService;
        private readonly RecipeHelperIdentityDbContext _dbContext;

        public IdentityRoleService(RoleManager<ApplicationRole> roleManager,
                                   UserManager<ApplicationUser> userManager,
                                   ILogger<IdentityRoleService> logger,
                                   IHttpContextUserService userService,
                                   RecipeHelperIdentityDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _userService = userService;
            _dbContext = dbContext;
        }


        #region Queries

        public async Task<IReadOnlyList<ApplicationRoleResponse>> GetAllAsync()
        {
            var roleListResponse = new List<ApplicationRoleResponse>();

            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                if (!roles.Any())
                {
                    _logger.LogInformation($"GetAllRolesAsync didn't return any roles for user with id: { _userService.GetUser() } ");
                    throw new Exception("No Roles in the application was found, ask the administrator for help.");
                }

                foreach (var role in roles)
                {
                    var roleResponse = new ApplicationRoleResponse
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Users = _userManager.Users
                            .Include(usr => usr.UserRoles!)
                            .ThenInclude(ur => ur.Role)
                            .Select(x => new UserInRoleDto
                            {
                                FirstName = x.FirstName ?? "",
                                LastName = x.LastName ?? "",
                                UserId = x.Id,
                                UserName = x.UserName ?? ""
                            }).ToList(),
                        Success = true
                    };

                    roleListResponse.Add(roleResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unhandled exception occurred in IdentityRoleService", ex.Message);
                throw new Exception("An unhandled exception got caught", ex);
            }

            return roleListResponse;
        }

        public async Task<ApplicationRoleResponse> GetByIdAsync(string id)
        {
            var role = await _roleManager.Roles.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (role is null)
            {
                return new ApplicationRoleResponse
                {
                    Success = false,
                    Errors = new[] { $"There is no role attached to the provided id: '{id}'." }
                };
            }

            return new ApplicationRoleResponse
            {
                Id = role!.Id,
                Name = role.Name,
                Users = await _userManager.Users.Include(usr => usr.UserRoles!).ThenInclude(ur => ur.Role)
                    .Select(x => new UserInRoleDto
                    {
                        FirstName = x.FirstName ?? "",
                        LastName = x.LastName ?? "",
                        UserId = x.Id,
                        UserName = x.UserName ?? ""
                    }).ToListAsync(),
                Success = true,
            };
        }

        #endregion

        #region Commands

        public async Task<ApplicationRoleResponse> DeleteAsync(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);
            var response = new ApplicationRoleResponse();

            // Check if role exists
            if (roleToDelete is null)
            {
                response.Success = false;
                response.Errors = new[]
                {
                    $"Role with id: {id} could not be found - you can't delete what you don't have."
                };

                return response;
            }

            // Check if any users belong to this role
            if (_dbContext.UserRoles.Where(x => x.RoleId == id).Any())
            {
                response.Success = false;
                response.Errors = new[]
                {
                    $"Role with id {id} could not be deleted. Users exists."
                };

                return response;
            }

            try
            {
                await _roleManager.DeleteAsync(roleToDelete);
                response.Success = true;
                response.Id = roleToDelete.Id;
                response.Name = roleToDelete.Name;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Delete process went south.", ex.Message);
            }

            return response;

        }

        public async Task<ApplicationRoleResponse> CreateAsync(CreateRoleRequest request)
        {
            var newRole = new ApplicationRole { Name = request.Name };
            var response = new ApplicationRoleResponse();

            try
            {
                await _roleManager.CreateAsync(newRole);
                response.Success = true;
                response.Name = newRole.Name;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Create process went south.", ex.Message);
            }

            return response;

        }

        public async Task<ApplicationRoleResponse> UpdateAsync(UpdateRoleRequest request)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id);
            var response = new ApplicationRoleResponse();

            // Check for null
            if (roleToUpdate is null)
            {
                response.Success = false;
                response.Errors = new[]
                {
                    $"Role with id: {request.Id} could not be found - you can't delete what you don't have."
                };

                return response;
            }

            try
            {
                roleToUpdate.Name = request.Name;
                roleToUpdate.NormalizedName = request.Name.ToUpper();
                await _roleManager.UpdateAsync(roleToUpdate);

                response.Success = true;
                response.Id = roleToUpdate.Id;
                response.Name = roleToUpdate.Name;
                
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Update process went south.", ex.Message);
            }

            return response;
        }

        #endregion
    }
}
