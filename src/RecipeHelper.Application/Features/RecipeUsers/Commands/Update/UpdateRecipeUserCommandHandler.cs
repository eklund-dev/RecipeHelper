using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Common.Helpers;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.RecipeUsers.Commands.Delete
{
    public class UpdateRecipeUserCommandHandler : IRequestHandler<UpdateRecipeUserCommand, Response<RecipeUserDto>>
    {
        private readonly IRecipeUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRecipeUserCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;
        private readonly IRecipeRepository _recipeRepository;

        public UpdateRecipeUserCommandHandler(
            IRecipeUserRepository repository,
            IMapper mapper,
            ILogger<UpdateRecipeUserCommandHandler> logger,
            IHttpContextUserService userService,
            IRecipeRepository recipeRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
            _recipeRepository = recipeRepository;
        }

        public async Task<Response<RecipeUserDto>> Handle(UpdateRecipeUserCommand request, CancellationToken cancellationToken)
        {
            var recipeUser = await _repository.GetByIdAsync(request.Id);
            if (recipeUser == null)
                return Response<RecipeUserDto>.Fail($"REcipeUser with id {request.Id} could not be found");

            var userName = _userService.GetClaimsUserName();

            try 
            {
                recipeUser.Name = request.Name.ToTitleCase();
                recipeUser.LastmodifiedBy = userName;
                recipeUser.LastModifiedDate = DateTime.UtcNow;
                recipeUser.FavoriteRecipes ??= new List<FavoriteRecipe>();

                foreach (var recipeId in request.RecipeIds)
                {
                    if (await _recipeRepository.GetByIdAsync(recipeId) is not null)
                    {
                        recipeUser.FavoriteRecipes.Add(new FavoriteRecipe
                        {
                            RecipeId = recipeId,
                            RecipeUserId = recipeUser.Id
                        });
                    }
                }

                await _repository.UpdateAync(recipeUser);

                return Response<RecipeUserDto>.Success(_mapper.Map<RecipeUserDto>(recipeUser), "RecipeUser Successfully updated");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error caught in {nameof(UpdateRecipeUserCommandHandler)} by user: {userName}.");
                throw new ApiException("Update RecipeUser Failed", ex.Message);
            }
        }
    }
}
