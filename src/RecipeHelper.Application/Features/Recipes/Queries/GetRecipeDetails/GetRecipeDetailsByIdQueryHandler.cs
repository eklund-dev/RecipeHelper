using AutoMapper;
using MediatR;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails
{
    public class GetRecipeDetailsByIdQueryHandler : IRequestHandler<GetRecipeDetailsByIdQuery, Response<RecipeDto>>
    {
        private readonly IAsyncRepository<Recipe, Guid> _repository;
        private readonly IMapper _mapper;

        public GetRecipeDetailsByIdQueryHandler(IAsyncRepository<Recipe, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<RecipeDto>> Handle(GetRecipeDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _repository.GetByIdAsync(request.Id);

            if (recipe == null)
                return Response<RecipeDto>.Fail($"Recipe with id {request.Id} not found");


            return Response<RecipeDto>.Success(_mapper.Map<RecipeDto>(recipe), "Recipe fetched");

        }
    }
}
