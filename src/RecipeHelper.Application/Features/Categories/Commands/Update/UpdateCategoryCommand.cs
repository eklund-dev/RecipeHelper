﻿using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Categories.Commands.Create
{
    public class UpdateCategoryCommand : IRequest<Response<CategoryDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
