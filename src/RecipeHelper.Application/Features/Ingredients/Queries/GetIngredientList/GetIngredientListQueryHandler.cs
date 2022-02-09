using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.Ingredients.Queries.GetIngredientList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, Response<PaginatedList<IngredientDto>>>
    {
        private readonly IAsyncRepository<Ingredient, Guid> _repository;
        private readonly IMapper _mapper;

        public GetCategoryListQueryHandler(IAsyncRepository<Ingredient, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<PaginatedList<IngredientDto>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var ingredients = _repository.Entity.ProjectTo<IngredientDto>(_mapper.ConfigurationProvider);

            var queryData = await PaginatedList<IngredientDto>.CreateFromEfQueryableAsync(ingredients.AsNoTracking(), request.QueryParameters.PageNumber, request.QueryParameters.PageSize);

            return Response<PaginatedList<IngredientDto>>.Success(queryData, "Succeeded");

        }
    }
}
