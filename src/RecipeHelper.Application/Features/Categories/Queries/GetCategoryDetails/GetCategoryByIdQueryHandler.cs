using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextUserService _userService;
        private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

        public GetCategoryByIdQueryHandler(
            ICategoryRepository repository,
            IMapper mapper,
            IHttpContextUserService userService,
            ILogger<GetCategoryByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        public async Task<Response<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);

            if (category == null)
                return Response<CategoryDto>.Fail($"Recipe with id {request.Id} not found");

            string userName = _userService.GetClaimsUserName();

            try
            {
                var dto = _mapper.Map<CategoryDto>(category);
                return Response<CategoryDto>.Success(dto, $"Recipe fetched by {userName}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCategoryByIdQueryHandler)} triggered by {userName}", ex.Message);
                throw new ApiException($"Error in {nameof(GetCategoryByIdQueryHandler)}");
            }

        }
    }
}
