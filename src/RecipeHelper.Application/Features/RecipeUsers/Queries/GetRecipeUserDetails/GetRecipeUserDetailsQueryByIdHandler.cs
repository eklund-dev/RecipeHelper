using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.RecipeUsers.Queries.GetRecipeUserDetails
{
    public class GetRecipeUserDetailsByIdQueryHandler : IRequestHandler<GetRecipeUserDetailsByIdQuery, Response<RecipeUserDto>>
    {
        private readonly IRecipeUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRecipeUserDetailsByIdQueryHandler> _logger;
        private readonly IHttpContextUserService _userService;
        public GetRecipeUserDetailsByIdQueryHandler(
            IRecipeUserRepository repository,
            IMapper mapper,
            ILogger<GetRecipeUserDetailsByIdQueryHandler> logger,
            IHttpContextUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }
        public async Task<Response<RecipeUserDto>> Handle(GetRecipeUserDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var recipeUser = await _repository.GetByIdAsync(request.Id);
            var userName = _userService.GetClaimsUserName();
            var recipeUserDto = _mapper.Map<RecipeUserDto>(recipeUser);

            if (recipeUser == null)
                return Response<RecipeUserDto>.Fail($"RecipeUser with id {request.Id} could not be found");

            return Response<RecipeUserDto>.Success(recipeUserDto, $"RecipeUser fetched Successfully by {userName}");
        }
    }
}
