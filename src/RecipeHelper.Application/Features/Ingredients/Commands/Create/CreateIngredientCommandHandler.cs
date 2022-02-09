using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Ingredients.Commands.Create
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Response<IngredientDto>>
    {
        private readonly IIngredientRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateIngredientCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;

        public CreateIngredientCommandHandler(
            IIngredientRepository repository,
            IMapper mapper,
            ILogger<UpdateIngredientCommandHandler> logger, IHttpContextUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }
        public async Task<Response<IngredientDto>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var userName = _userService.GetClaimsUserName();

            var newIngredient = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedBy = userName,
                CreatedDate = DateTime.UtcNow
            };

            try
            {
                await _repository.AddAsync(newIngredient);
                _logger.LogInformation($"New ingredient: {newIngredient.Name} Created by {userName}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ApiException($"Exception thrown in {nameof(CreateIngredientCommandHandler)}", ex.Message);
            }

            return Response<IngredientDto>.Success($"New ingredient: {newIngredient.Name} created by {userName}.");

        }
    }
}
