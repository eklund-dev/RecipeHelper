using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.RecipeUsers.Commands.Delete
{
    public class DeleteRecipeUserCommandHandler : IRequestHandler<DeleteRecipeUserCommand, Response<RecipeUserDto>>
    {
        private readonly IRecipeUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRecipeUserCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;

        public DeleteRecipeUserCommandHandler(
            IRecipeUserRepository repository,
            IMapper mapper,
            ILogger<UpdateRecipeUserCommandHandler> logger,
            IHttpContextUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }
        public async Task<Response<RecipeUserDto>> Handle(DeleteRecipeUserCommand request, CancellationToken cancellationToken)
        {
            var recipeUser = await _repository.GetByIdAsync(request.Id);
            
            if (recipeUser == null)
                return Response<RecipeUserDto>.Fail($"RecipeUser with id {request.Id} could not be found");

            var userName = _userService.GetClaimsUserName();
            try
            {
                await _repository.DeleteAsync(recipeUser);
                _logger.LogInformation($"RecipeUser with id {recipeUser.Id} deleted by {userName}");
                return Response<RecipeUserDto>.Success($"RecipeUser with id {recipeUser.Id} deleted successfully by {userName}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error caught in {nameof(DeleteRecipeUserCommandHandler)} by user {userName}");
                throw new ApiException($"Error caught in {nameof(DeleteRecipeUserCommandHandler)}", ex.Message);
            }
        }
    }
}
