using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity.Roles;
using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.Requests.Roles;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Common.Responses.Identity;
using RecipeHelper.Domain.Exceptions;
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

        public async Task<Response<PaginatedList<ApplicationRoleDto>>> GetAllAsync(int? pageNumber, int? pageSize)
        {
            var roleList = new List<ApplicationRoleDto>();
            PaginatedList<ApplicationRoleDto> paginatedList;

            try
            {

                var roles = _dbContext.Roles.Include(x => x.UserRoles).ThenInclude(us => us.User).Select(x => new ApplicationRoleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Users = x.UserRoles.Select(xx => new UserInRoleDto
                    {
                        FirstName = xx.User.FirstName ?? "",
                        LastName = xx.User.LastName ?? "",
                        UserId = xx.UserId,
                        UserName = xx.User.UserName
                    })
                });

                //Refaktorera denn

                if(roles.GetEnumerator().MoveNext() is false)
                {
                    _logger.LogInformation($"GetAllRolesAsync didn't return any roles for user with id: { _userService.GetUser() } ");
                    throw new Exception("No Roles in the application was found, ask the administrator for help.");
                }

                paginatedList = await PaginatedList<ApplicationRoleDto>.CreateFromEfQueryableAsync(roles, pageNumber ?? 1, pageSize ?? 10);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unhandled exception occurred in IdentityRoleService", ex.Message);
                throw new ApplicationRoleViolationException("An unhandled exception got caught", ex);
            }

            return Response<PaginatedList<ApplicationRoleDto>>.Success(paginatedList, "Succeeded");
        }

        public async Task<Response<ApplicationRoleDto>> GetByIdAsync(string id)
        {
            var role = await _roleManager.Roles.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (role is null) return Response<ApplicationRoleDto>.Fail($"There is no role attached to the provided id: '{id}'.");

            var applicationRoleDto = new ApplicationRoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Users = await _userManager.Users
                    .Include(usr => usr.UserRoles!)
                    .ThenInclude(ur => ur.Role)
                    .Select(x => new UserInRoleDto
                    {
                        FirstName = x.FirstName ?? "",
                        LastName = x.LastName ?? "",
                        UserId = x.Id,
                        UserName = x.UserName ?? ""
                    }).ToListAsync(),
            };

            return Response<ApplicationRoleDto>.Success(applicationRoleDto, "Role fetched");
         
        }

        #endregion

        #region Commands

        public async Task<Response<ApplicationRoleDto>> DeleteAsync(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);

            // Check if role exists
            if (roleToDelete is null) 
                return Response<ApplicationRoleDto>.Fail($"Role with id: {id} could not be found - you can't delete what you don't have.");
           
            // Check if any users belong to this role
            if (_dbContext.UserRoles.Where(x => x.RoleId == id).Any())
                return Response<ApplicationRoleDto>.Fail($"Role with id {id} could not be deleted. Users exists.");

            try
            {
                await _roleManager.DeleteAsync(roleToDelete);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Delete process went south.", ex.Message);
            }

            return Response<ApplicationRoleDto>.Success("Role successfully deleted");

        }

        public async Task<Response<ApplicationRoleDto>> CreateAsync(CreateRoleRequest request)
        {
            var newRole = new ApplicationRole { Name = request.Name };
            var dto = new ApplicationRoleDto();

            try
            {
                await _roleManager.CreateAsync(newRole);
                dto.Name = request.Name;
                dto.Id = newRole.Id;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Create process went south.", ex.Message);
            }

            return Response<ApplicationRoleDto>.Success(dto, "Role created");

        }

        public async Task<Response<ApplicationRoleDto>> UpdateAsync(UpdateRoleRequest request)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id);
            var dto = new ApplicationRoleDto();

            // Check for null
            if (roleToUpdate is null) 
                return Response<ApplicationRoleDto>.Fail($"Role with id: {request.Id} could not be found - you can't delete what you don't have.");

            try
            {
                roleToUpdate.Name = request.Name;
                roleToUpdate.NormalizedName = request.Name.ToUpper();
                await _roleManager.UpdateAsync(roleToUpdate);

                dto.Id = roleToUpdate.Id;
                dto.Name = roleToUpdate.Name;
                
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Update process went south.", ex.Message);
            }

            return Response<ApplicationRoleDto>.Success(dto, "Role updated");
        }

        #endregion
    }
}
