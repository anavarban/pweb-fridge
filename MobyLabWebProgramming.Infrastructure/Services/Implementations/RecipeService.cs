using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{

    public class RecipeService : IRecipeService
    {
    private readonly IRepository<WebAppDatabaseContext> _repository;

        public RecipeService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<RecipeDTO>> GetRecipeWithIngredient(IngredientDTO ingredient, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(new RecipeProjectionSpec(ingredient: new Ingredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                ExpiryDate = ingredient.ExpiryDate,
                Recipes = ingredient.Recipes
            }), cancellationToken);

            return result != null ?
                ServiceResponse<RecipeDTO>.ForSuccess(result) :
                ServiceResponse<RecipeDTO>.FromError(new(HttpStatusCode.NotFound, "Recipe not found!", ErrorCodes.EntityNotFound));
        }

        public async Task<ServiceResponse> CreateRecipe(RecipeDTO recipe, CancellationToken cancellationToken)
        {
            var result = await _repository.AddAsync(new Recipe
            {
                Name = recipe.Name,
                Description = recipe.Description,
                ImagePath = recipe.ImagePath,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions
            }, cancellationToken);

            return ServiceResponse.ForSuccess();
        }
    }
}
