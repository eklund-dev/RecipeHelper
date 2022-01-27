using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Ingredients.Validators;
using RecipeHelper.Domain.Entities;
using System.Text.Json;

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
            //    var validator = new CreateIngredientValidator(_repository);

            //    var result = await validator.ValidateAsync(request, cancellationToken);

            //    if (result.Errors.Count > 0)
            //        return Response<IngredientDto>.Fail("Validation didnt pass.", JsonSerializer.Serialize(result.Errors).Split(",").ToList());

            var newIngredient = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedBy = _userService.GetUser()?.ToString() ?? "Anonymous",
                CreatedDate = DateTime.UtcNow
            };

            try
            {
                await _repository.AddAsync(newIngredient);
                _logger.LogInformation($"New ingredient: {newIngredient.Name} Created by {_userService.GetUser()?.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return Response<IngredientDto>.Success("New ingredient created");

        }
    }
}
