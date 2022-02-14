using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Categories.Commands.Create;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;

        public UpdateCategoryCommandHandler(
            ICategoryRepository repository,
            IMapper mapper,
            ILogger<UpdateCategoryCommandHandler> logger,
            IHttpContextUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        public async Task<Response<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var userName = _userService.GetClaimsUserName();

            try
            {
                var category = await _repository.GetByIdAsync(request.Id);
                if (category == null)
                    return Response<CategoryDto>.Fail($"Category with id {request.Id} could not be found");

                category.Name = request.Name;
                await _repository.UpdateAync(category);
                var categoryDto = _mapper.Map<CategoryDto>(category);
                _logger.LogInformation($"Category {category.Name} updated by {userName}.");
                return Response<CategoryDto>.Success(categoryDto, "Category updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {nameof(UpdateCategoryCommandHandler)} by {userName}.");
                throw new ApiException($"Exception occured in {nameof(UpdateCategoryCommandHandler)}", ex.Message);
            }
        }
    }
}
