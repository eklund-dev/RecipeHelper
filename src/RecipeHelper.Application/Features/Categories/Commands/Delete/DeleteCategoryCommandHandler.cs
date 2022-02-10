using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Categories.Commands.Create;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;

        public DeleteCategoryCommandHandler(
            ICategoryRepository repository,
            IMapper mapper,
            ILogger<CreateCategoryCommandHandler> logger,
            IHttpContextUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        public async Task<Response<CategoryDto>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category is null)
                return Response<CategoryDto>.Fail($"Category with id {request.Id} could not be found");

            var userName = _userService.GetClaimsUserName();

            try
            {
                await _repository.DeleteAsync(category);
                _logger.LogInformation($"Category with id {category.Id} deleted by {userName}");
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return Response<CategoryDto>.Success(categoryDto, $"Deleted successfully");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error when deleting ingredient by {userName}", ex.Message);
                throw new ApiException($"Error when deleting ingredient by {userName}", ex.Message);
            }

        }
    }
}
