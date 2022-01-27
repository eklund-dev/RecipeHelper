using AutoMapper;
using MediatR;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Features.Ingredients.Queries.GetIngredientDetails
{
    public class GetIngredientDetailsByIdQueryHandler : IRequestHandler<GetIngredientDetailsByIdQuery, Response<IngredientDto>>
    {
        private readonly IAsyncRepository<Ingredient, Guid> _repository;
        private readonly IMapper _mapper;
        public GetIngredientDetailsByIdQueryHandler(IAsyncRepository<Ingredient, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<IngredientDto>> Handle(GetIngredientDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var ingredient = await _repository.GetByIdAsync(request.Id);

            if (ingredient == null)
                return Response<IngredientDto>.Fail($"Recipe with id {request.Id} not found");


            return Response<IngredientDto>.Success(_mapper.Map<IngredientDto>(ingredient), "Recipe fetched");
        }
    }
}