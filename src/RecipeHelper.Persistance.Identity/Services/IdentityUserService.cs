﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity;
using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.Requests.Users;
using RecipeHelper.Application.Common.Responses;
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
        public async Task<Response<PaginatedList<ApplicationUserDto>>> GetPaginatedUsersAsync(int? pageNumber, int? pageSize)
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<ApplicationUser, ApplicationUserDto>());

            var users = _userManager.Users.ProjectTo<ApplicationUserDto>(configuration);

            var rs = await PaginatedList<ApplicationUserDto>.CreateFromEfQueryableAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize ?? 10);

            return Response<PaginatedList<ApplicationUserDto>>.Success(rs, "Succeeded");
        }
        
        public async Task<Response<ApplicationUserDto>> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user is null ?
                Response<ApplicationUserDto>.Fail($"User with id: {id} doesn't exist.") :
                Response<ApplicationUserDto>.Success(new ApplicationUserDto
                {
                    Id = id,
                    UserName = user.UserName,
                    FirstName = user.FirstName ?? "",
                    LastName = user.LastName ?? "",
                    Email = user.Email!,
                    EmailConfirmed = user.EmailConfirmed,
                    Roles = await _userManager.GetRolesAsync(user)
                }, "User Successfully fetched");
           
        }

        #endregion

        #region Commands
        public async Task<Response<ApplicationUserDto>> CreateUserAsync(CreateUserRequest request)
        {
            if (request is null) 
                return Response<ApplicationUserDto>.Fail("Request appeared to empty, can't work with that you know.");

            try
            {
                var newAppUser = new ApplicationUser
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.UserName,
                };

                await _userManager.CreateAsync(newAppUser, request.Password);
                _logger.LogInformation("New User has been created");
            }
            catch (Exception ex)
            {
                throw new ApplicationUserViolationException("A new user could not be created", ex.Message);
            }

            return Response<ApplicationUserDto>.Success("New user created.");

        }

        public async Task<Response<ApplicationUserDto>> UpdateUserAsync(UpdateUserRequest request)
        {
            if (request is null)
                return Response<ApplicationUserDto>.Fail("Request appeared to empty, can't work with that you know.");

            if(await _userManager.FindByIdAsync(request.Id) is null)
                return Response<ApplicationUserDto>.Fail("User does not exist.");

            try
            {
                var mappedUser = _mapper.Map<ApplicationUser>(request);
                await _userManager.UpdateAsync(mappedUser);
                _logger.LogInformation($"User with Id {request.Id} has been updated succesfully.");
                return Response<ApplicationUserDto>.Success(_mapper.Map<ApplicationUserDto>(mappedUser), "User Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in UpdateAsync");
                throw new ApplicationRoleViolationException("Error occurred in UpdateAsync", ex.Message);
            }

        }
        public async Task<Response<ApplicationUserDto>> DeleteUserAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Response<ApplicationUserDto>.Fail($"Id: {id} is either null or white space, try it again.");

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) 
                    return Response<ApplicationUserDto>.Fail($"User with id {id} could not be found");

                var userRoles = await _userManager.GetRolesAsync(user);
                userRoles.ToList().ForEach(async role => await _userManager.RemoveFromRoleAsync(user, role));

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded is false)
                    return Response<ApplicationUserDto>.Fail("Could not delete user");

                return Response<ApplicationUserDto>.Success($"User with id: {id} successfully deleted by {_userService.GetUser}");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in DeleteAsync");
                throw new ApplicationRoleViolationException("Error occurred in DeleteAsync", ex.Message);
            }
            
        }

        // Borde refaktorera denna
        public async Task<Response<ApplicationUserDto>> ManageUserRoleAsync(ManageUserRoleRequest request)
        {
            if (request is null) 
                return Response<ApplicationUserDto>.Fail("The request is empty, what are you doing?");
            
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user is null)
                    return Response<ApplicationUserDto>.Fail($"The user with id {request.UserId} could not be found");

                var existingRoles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);

                if (result.Succeeded is false)
                    return Response<ApplicationUserDto>.Fail($"Step 1 'Removing existing roles' -> Could not change roles for user with id {user.Id}");

                result = await _userManager.AddToRolesAsync(user, request.Roles);
                    
                if (result.Succeeded is false)
                    return Response<ApplicationUserDto>.Fail($"Step 2: 'Adding new roles' -> Could not change roles for user with id {user.Id}");

                if (existingRoles.Contains(Enum.GetName(typeof(RoleType), RoleType.Owner)) &&
                await _userManager.IsInRoleAsync(user, "Owner") is false)
                {
                    return Response<ApplicationUserDto>.Fail($"User with id: {user.Id} is the one and only OWNER and could therefore not lose the title through this api call");
                }

                return Response<ApplicationUserDto>.Success($"Roles has been updated for User: {user.UserName}.");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred in {nameof(ManageUserRoleAsync)}");
                throw new ApplicationRoleViolationException("Error occurred in DeleteAsync", ex.Message);
            }

        }

        #endregion
    }
}
