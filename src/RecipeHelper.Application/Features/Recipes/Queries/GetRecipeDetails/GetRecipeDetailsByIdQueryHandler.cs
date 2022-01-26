using AutoMapper;
using MediatR;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails
{
    public class GetRecipeDetailsByIdQueryHandler : IRequestHandler<GetRecipeDetailsByIdQuery, Response<RecipeQueryDto>>
    {
        private readonly IAsyncReadRepository<Recipe> _repository;
        private readonly IMapper _mapper;

        public GetRecipeDetailsByIdQueryHandler(IAsyncReadRepository<Recipe> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<RecipeQueryDto>> Handle(GetRecipeDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _repository.GetByIdAsync(request.Id);

            if (recipe == null)
                return Response<RecipeQueryDto>.Fail($"Recipe with id {request.Id} not found");


            return Response<RecipeQueryDto>.Success(_mapper.Map<RecipeQueryDto>(recipe), "Recipe fetched");

        }
    }
}
