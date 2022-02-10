using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Recipes.Commands.Delete
{
    public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand, Response<RecipeDto>>
    {
        private readonly IRecipeRepository _repoository;
        private readonly IHttpContextUserService _userService;
        private readonly ILogger<DeleteRecipeCommandHandler> _logger;

        public DeleteRecipeCommandHandler(
            IRecipeRepository repoository,
            IHttpContextUserService userService,
            ILogger<DeleteRecipeCommandHandler> logger)
        {
            _repoository = repoository;
            _userService = userService;
            _logger = logger;
        }
        public async Task<Response<RecipeDto>> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var userName = _userService.GetClaimsUserName();

            try
            {
                var recipe = await _repoository.GetByIdAsync(request.Id);
                if (recipe == null)
                    return Response<RecipeDto>.Fail($"Recipe with id {request.Id} could not be found");

                await _repoository.DeleteAsync(recipe);
                _logger.LogInformation($"Recipe with id {request.Id} successfully deleted by {userName}");
                return Response<RecipeDto>.Success($"Recipe with id {request.Id} successfully deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception appeared in {nameof(DeleteRecipeCommandHandler)} - caused by: {userName}", ex.Message);

                throw new ApiException($"Error caught in {nameof(DeleteRecipeCommandHandler)}", ex.Message);
            }
        }
    }
}
