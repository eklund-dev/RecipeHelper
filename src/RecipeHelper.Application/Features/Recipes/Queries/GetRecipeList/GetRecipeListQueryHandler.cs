using AutoMapper;
using MediatR;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Application.Recipes.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList
{
    public class GetRecipeListQueryHandler : IRequestHandler<GetRecipeListQuery, RecipeQueryAllResponse>
    {
        private readonly IAsyncReadRepository<Recipe, Guid> _repository;
        private readonly IMapper _mapper;

        public GetRecipeListQueryHandler(IAsyncReadRepository<Recipe, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RecipeQueryAllResponse> Handle(GetRecipeListQuery request, CancellationToken cancellationToken)
        {
            var listofRecipes = (await _repository.ListAllAsync()).ToList();

            return listofRecipes.Any() ? new RecipeQueryAllResponse
            {
                Success = true,
                Data = _mapper.Map<List<RecipeQueryDto>>(listofRecipes)
            } 
            : new RecipeQueryAllResponse
            {
                Success = false,
                Errors = new [] { "No Entities found" }
            };
        }
    }
}
