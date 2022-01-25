using AutoMapper;
using MediatR;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Application.Recipes.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails
{
    public class GetRecipeDetailsByIdQueryHandler : IRequestHandler<GetRecipeDetailsByIdQuery, RecipeQueryResponse>
    {
        private readonly IAsyncReadRepository<Recipe, Guid> _repository;
        private readonly IMapper _mapper;

        public GetRecipeDetailsByIdQueryHandler(IAsyncReadRepository<Recipe, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RecipeQueryResponse> Handle(GetRecipeDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _repository.GetByIdAsync(request.Id);

            if (recipe == null) throw new ArgumentNullException(nameof(recipe));
           
            var recipeQueryResponse = new RecipeQueryResponse
            {
                Success = true,
                Data = _mapper.Map<RecipeQueryDto>(recipe)
            };

            return recipeQueryResponse;
        }
    }
}
