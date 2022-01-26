using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList
{
    public class GetRecipeListQueryHandler : IRequestHandler<GetRecipeListQuery, Response<PaginatedList<RecipeQueryDto>>>
    {
        private readonly IAsyncReadRepository<Recipe> _repository;
        private readonly IMapper _mapper;

        public GetRecipeListQueryHandler(IAsyncReadRepository<Recipe> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedList<RecipeQueryDto>>> Handle(GetRecipeListQuery request, CancellationToken cancellationToken)
        {
            var recipes = _repository.Entity.ProjectTo<RecipeQueryDto>(_mapper.ConfigurationProvider);

            var queryData = await PaginatedList<RecipeQueryDto>.CreateFromEfQueryableAsync(recipes.AsNoTracking(), request.Parameters.PageNumber, request.Parameters.PageSize);

            return Response<PaginatedList<RecipeQueryDto>>.Success(queryData, "Succeeded");
          
        }
    }
}
