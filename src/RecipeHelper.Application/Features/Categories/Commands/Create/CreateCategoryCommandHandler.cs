using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;
        private readonly IHttpContextUserService _userService;

        public CreateCategoryCommandHandler(
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

        public async Task<Response<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var userName = _userService.GetClaimsUserName();

            try
            {
                var category = _mapper.Map<Category>(request);
                category.CreatedBy = userName;
                category.CreatedDate = DateTime.UtcNow;
                
                await _repository.AddAsync(category);
                var categoryDto = _mapper.Map<CategoryDto>(category);
                _logger.LogInformation($"New category: {category.Name} Created by {userName}.");
                return Response<CategoryDto>.Success(categoryDto, "New Category created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in {nameof(CreateCategoryCommandHandler)} by {userName}.");
                throw new ApiException($"Exception occured in {nameof(CreateCategoryCommandHandler)}", ex.Message);
            }
        }
    }
}
