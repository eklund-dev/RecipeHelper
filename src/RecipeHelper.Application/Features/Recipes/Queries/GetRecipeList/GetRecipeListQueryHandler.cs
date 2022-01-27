using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList
{
    public class GetRecipeListQueryHandler : IRequestHandler<GetRecipeListQuery, Response<PaginatedList<RecipeDto>>>
    {
        private readonly IAsyncRepository<Recipe, Guid> _repository;
        private readonly IMapper _mapper;

        public GetRecipeListQueryHandler(IAsyncRepository<Recipe, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedList<RecipeDto>>> Handle(GetRecipeListQuery request, CancellationToken cancellationToken)
        {
            var recipes = _repository.Entity.ProjectTo<RecipeDto>(_mapper.ConfigurationProvider);

            var queryData = await PaginatedList<RecipeDto>.CreateFromEfQueryableAsync(recipes.AsNoTracking(), request.QueryParameters.PageNumber, request.QueryParameters.PageSize);

            return Response<PaginatedList<RecipeDto>>.Success(queryData, "Succeeded");
          
        }
    }
}
