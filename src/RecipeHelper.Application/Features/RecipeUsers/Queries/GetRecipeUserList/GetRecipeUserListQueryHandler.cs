using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.RecipeUsers.Queries.GetRecipeUserList
{
    public class GetRecipeUserListQueryHandler : IRequestHandler<GetRecipeUserListQuery, Response<PaginatedList<RecipeUserDto>>>
    {
        private readonly IRecipeUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRecipeUserListQueryHandler> _logger;
        private readonly IHttpContextUserService _userService;
        public GetRecipeUserListQueryHandler(
            IHttpContextUserService userService,
            ILogger<GetRecipeUserListQueryHandler> logger,
            IMapper mapper,
            IRecipeUserRepository repository)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Response<PaginatedList<RecipeUserDto>>> Handle(GetRecipeUserListQuery request, CancellationToken cancellationToken)
        {
            var recipeUsers = await _repository.ListAllAsync();

            if (!recipeUsers.Any())
            {
                return Response<PaginatedList<RecipeUserDto>>.Fail("There are no Recipeusers registered.");
            }

            var recipeUserListQuery = _repository.Entity.ProjectTo<RecipeUserDto>(_mapper.ConfigurationProvider);

            var queryData = await PaginatedList<RecipeUserDto>.CreateFromEfQueryableAsync(recipeUserListQuery.AsNoTracking(), request.QueryParameters.PageNumber, request.QueryParameters.PageSize);

            return Response<PaginatedList<RecipeUserDto>>.Success(queryData, "Succeeded");
        }
    }
}
