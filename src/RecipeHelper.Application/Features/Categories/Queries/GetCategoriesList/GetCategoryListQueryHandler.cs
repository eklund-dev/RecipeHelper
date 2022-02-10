using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Exceptions;

namespace RecipeHelper.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, Response<PaginatedList<CategoryDto>>>
    {
        private readonly IAsyncRepository<Category, Guid> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoryListQueryHandler> _logger;
        private readonly IHttpContextUserService _userService;

        public GetCategoryListQueryHandler(
            IAsyncRepository<Category, Guid> repository, 
            IMapper mapper, 
            ILogger<GetCategoryListQueryHandler> logger, 
            IHttpContextUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }
        public async Task<Response<PaginatedList<CategoryDto>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var userName = string.Empty;

            try
            {
                userName = _userService.GetClaimsUserName();
                var categories = _repository.Entity.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider);
                var queryData = await PaginatedList<CategoryDto>.CreateFromEfQueryableAsync(categories.AsNoTracking(), request.QueryParameters.PageNumber, request.QueryParameters.PageSize);
                return Response<PaginatedList<CategoryDto>>.Success(queryData, "Fetch Succeeded");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCategoryListQueryHandler)} triggered by {userName}", ex.Message);
                throw new ApiException($"Error in {nameof(GetCategoryListQueryHandler)}", ex.Message);
            }
            
        }
    }
}
