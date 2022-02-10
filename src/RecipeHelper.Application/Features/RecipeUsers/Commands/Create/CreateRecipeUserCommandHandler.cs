using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.RecipeUsers.Commands.Create
{
    public class CreateRecipeUserCommandHandler : IRequestHandler<CreateRecipeUserCommand, Response<RecipeUserDto>>
    {
        private readonly IRecipeUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRecipeUserCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;
        public CreateRecipeUserCommandHandler(
            IRecipeUserRepository repository,
            IMapper mapper,
            ILogger<CreateRecipeUserCommandHandler> logger,
            IHttpContextUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }
        public async Task<Response<RecipeUserDto>> Handle(CreateRecipeUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetClaimsUserId();
            var userName = _userService.GetClaimsUserName();
            
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return Response<RecipeUserDto>.Fail($"Either userId: '{userId}' is empty or it could not be parsed to type 'Guid'");
            }

            var recipeUser = new RecipeUser
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ApplicationUserId = parsedUserId,
                CreatedBy = userName,
                CreatedDate = DateTime.UtcNow,
                FavoriteRecipes = null,
            };

            await _repository.AddAsync(recipeUser);
            var recipeUserDto = _mapper.Map<RecipeUserDto>(recipeUser);
            _logger.LogInformation($"A RecipeUser was added successfully by '{userName}'");
            return Response<RecipeUserDto>.Success(recipeUserDto, $"RecipeUser Successfully added by '{userName}'");
        }
    }
}
