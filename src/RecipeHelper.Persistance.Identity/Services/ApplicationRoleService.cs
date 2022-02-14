using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity.Roles;
using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Identity.Requests.Roles;
using RecipeHelper.Persistance.Identity.Configurations;
using RecipeHelper.Persistance.Identity.Context;
using RecipeHelper.Persistance.Identity.Models;
using RecipeHelper.Persistance.Identity.Validators;

namespace RecipeHelper.Persistance.Identity.Services
{
    public class ApplicationRoleService : IIdentityRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ApplicationRoleService> _logger;
        private readonly IHttpContextUserService _userService;
        private readonly IMapper _mapper;
        private readonly RecipeHelperIdentityDbContext _dbContext;

        public ApplicationRoleService(RoleManager<ApplicationRole> roleManager,
                                   UserManager<ApplicationUser> userManager,
                                   ILogger<ApplicationRoleService> logger,
                                   IHttpContextUserService userService,
                                   RecipeHelperIdentityDbContext dbContext, 
                                   IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _userService = userService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Queries

        public async Task<Response<PaginatedList<ApplicationRoleDto>>> GetAllAsync(int? pageNumber, int? pageSize)
        {
            if (!IdentityValidators.ValidateIntegerInput(pageNumber, pageSize))
                return Response<PaginatedList<ApplicationRoleDto>>.Fail("Error Occured", new List<string> { "You can not enter a number equal to or lower than zero" });

            var roles = _roleManager.Roles.ProjectTo<ApplicationRoleDto>(MapperConfig.GetRoleWithUsersConfig());

            var paginatedList = await PaginatedList<ApplicationRoleDto>.CreateFromEfQueryableAsync(roles, pageNumber ?? 1, pageSize ?? 10);

            return Response<PaginatedList<ApplicationRoleDto>>.Success(paginatedList, "Fetching Roles Succeeded.");
        }

        public async Task<Response<ApplicationRoleDto>> GetByIdAsync(string id)
        {
            if (await _roleManager.FindByIdAsync(id) is null)
                return Response<ApplicationRoleDto>.Fail($"Role with id {id} could not be found");

            var role = _roleManager.Roles.Where(x => x.Id == id)
                .ProjectTo<ApplicationRoleDto>(MapperConfig.GetRoleWithUsersConfig())
                .SingleOrDefault();

            if (role is null) 
                return Response<ApplicationRoleDto>.Fail($"There is no role attached to the provided id: '{id}'.");
            
            var roleDto = _mapper.Map<ApplicationRoleDto>(role);

            return Response<ApplicationRoleDto>.Success(role, "Role fetched"); ;   
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
                _logger.LogInformation($"Role with name {roleToDelete.Name} has been deleted by {_userService.GetClaimsUserName()}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in {nameof(DeleteAsync)}", ex.Message);
                throw new ArgumentException($"Process failed in {nameof(DeleteAsync)}.", ex.Message);
            }

            return Response<ApplicationRoleDto>.Success($"Role: {roleToDelete.Name} successfully deleted");

        }

        public async Task<Response<ApplicationRoleDto>> CreateAsync(CreateRoleRequest request)
        {
            try
            {
                var newRole = new ApplicationRole { Name = request.Name };
                await _roleManager.CreateAsync(newRole);

                _logger.LogInformation($"Role with name {newRole.Name} has been created by {_userService.GetClaimsUserName()}");
                return Response<ApplicationRoleDto>.Success(_mapper.Map<ApplicationRoleDto>(newRole), "Role created");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in {nameof(CreateAsync)}", ex.Message);
                throw new ArgumentException($"Role Create process failed in {nameof(CreateAsync)}.", ex.Message);
            }
        }

        public async Task<Response<ApplicationRoleDto>> UpdateAsync(UpdateRoleRequest request)
        {
            if (await _roleManager.FindByIdAsync(request.Id) is null)
                return Response<ApplicationRoleDto>.Fail($"Role with id: {request.Id} could not be found - you can't delete what you don't have.");

            try
            {
                var role = _mapper.Map<ApplicationRole>(request);
                await _roleManager.UpdateAsync(role);

                _logger.LogInformation($"Role with id {role.Id} has been updated by {_userService.GetClaimsUserName()}");
                return Response<ApplicationRoleDto>.Success(_mapper.Map<ApplicationRoleDto>(role), "Role updated");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in {nameof(UpdateAsync)}", ex.Message);
                throw new ArgumentException($"Process failed in {nameof(UpdateAsync)}", ex.Message);
            }

        }

        #endregion

    }
}
