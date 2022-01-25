using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity;
using RecipeHelper.Application.Common.Requests.Users;
using RecipeHelper.Application.Common.Responses.Identity;
using RecipeHelper.Domain.Exceptions;
using RecipeHelper.Persistance.Identity.Context;
using RecipeHelper.Persistance.Identity.Enums;
using RecipeHelper.Persistance.Identity.Models;

namespace RecipeHelper.Persistance.Identity.Services
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<IdentityUserService> _logger;
        private readonly IHttpContextUserService _userService;
        private readonly RecipeHelperIdentityDbContext _dbContext;
        private readonly IMapper _mapper;

        public IdentityUserService(
            UserManager<ApplicationUser> userManager,
            ILogger<IdentityUserService> logger,
            IHttpContextUserService userService,
            RecipeHelperIdentityDbContext dbContext,
            IMapper mapper, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _userService = userService;
            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        #region Queries
        public async Task<IReadOnlyList<ApplicationUserResponse>> GetAllAsync()
        {
            var users = await _userManager.Users
                .Select(user => new ApplicationUserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.FirstName ?? "",
                    LastName = user.LastName ?? "",
                    Roles = _userManager.GetRolesAsync(user).Result,
                    Success = true
                }).ToListAsync();

            return (users.Any() ? 
                users : 
                throw new ApplicationUserViolationException("No users found in the database"));
        }

        public async Task<ApplicationUserResponse> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user is not null ?
                new ApplicationUserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName ?? "",
                    LastName = user.LastName ?? "",
                    Email = user.Email!,
                    EmailConfirmed = user.EmailConfirmed,
                    Roles = await _userManager.GetRolesAsync(user)
                } :
                new ApplicationUserResponse
                {
                    Success = false,
                    Errors = new[] { $"User with id: {id} doesn't exist." }
                };
                //throw new ApplicationUserViolationException($"User with id: {id} doesn't exist.");
        }

        #endregion

        #region Commands
        public async Task<ApplicationUserResponse> CreateAsync(CreateUserRequest request)
        {
            var newAppUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
            };

            try
            {
                await _userManager.CreateAsync(newAppUser, request.Password);
                _logger.LogInformation("New User has been created");
            }
            catch (Exception ex)
            {
                throw new ApplicationUserViolationException("A new user could not be created", ex.Message);
            }

            return new ApplicationUserResponse
            {
                Success = true,
                Message = "A new user has been created flawlessly. Great job!"
            };
        }
        public async Task<ApplicationUserResponse> UpdateAsync(UpdateUserRequest request)
        {
            var userToUpdate = _userManager.FindByIdAsync(request.Id);
            var response = new ApplicationUserResponse();

            if(userToUpdate == null)
            {
                response.Success = false;
                response.Message = "User could not be updated. The id did not match.";
                return response;
            }

            try
            {
                var mappedUser = _mapper.Map<ApplicationUser>(request);
                await _userManager.UpdateAsync(mappedUser);
                _logger.LogInformation($"User with Id {request.Id} has been updated succesfully.");
                response.Success = true;
                response.Message = $"User with Id {request.Id} has been updated";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in UpdateAsync");
                throw new ApplicationRoleViolationException("Error occurred in UpdateAsync", ex.Message);
            }

            return response;

        }
        public async Task<ApplicationUserResponse> DeleteAsync(string id)
        {
            var response = new ApplicationUserResponse();
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                response.Success = false;
                response.Errors = new[] { $"User with id {id} could not be found" };
                return response;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);                
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = "User successfully deleted.";
            }

            response.Success = result.Succeeded;
            response.Message = "User successfully deleted";
            return response;
            
        }

        // Borde refaktorera denna
        public async Task<ApplicationUserResponse> ManageUserRoleAsync(ManageUserRoleRequest request)
        {
            var response = new ApplicationUserResponse();
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return response;
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);

            if (!result.Succeeded)
            {
                response.Success = false;
                response.Errors = new[] { $"Could not remove existing roles from user with id {user.Id}" };
            }

            result = await _userManager.AddToRolesAsync(user, request.Roles);
           
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Errors = new[] { $"Could not Add new roles for user with id {user.Id}" };
            }

            // Specialsteg för admins
            if (existingRoles.Contains(Enum.GetName(typeof(RoleType), RoleType.Admin)) && 
                await _userManager.IsInRoleAsync(user, "Admin") is false)
            {
                result = await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleType), RoleType.Admin));

                if (!result.Succeeded)
                {
                    response.Success = false;
                    response.Errors = new[] { $"User with id:{user.Id} is a former admin and could therefor not lose the title through this call" };
                }
            }

            response.Success = true;
            response.Roles = request.Roles;

            return response;
        }

        #endregion
    }
}
