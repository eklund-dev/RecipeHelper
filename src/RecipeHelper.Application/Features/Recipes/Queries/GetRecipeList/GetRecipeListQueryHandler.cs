using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using System.Diagnostics;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList
{
    public class GetRecipeListQueryHandler : IRequestHandler<GetRecipeListQuery, Response<PaginatedList<RecipeDto>>>
    {
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;

        public GetRecipeListQueryHandler(IRecipeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedList<RecipeDto>>> Handle(GetRecipeListQuery request, CancellationToken cancellationToken)
        {
            var queryableRecipes = _repository.Entity
                .Include(x => x.RecipeIngredients)
                .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider);

            var queryData = await PaginatedList<RecipeDto>.CreateFromEfQueryableAsync(queryableRecipes.AsNoTracking(), request.QueryParameters.PageNumber, request.QueryParameters.PageSize);

            return Response<PaginatedList<RecipeDto>>.Success(queryData, "Succeeded");
          
        }
    }
}
