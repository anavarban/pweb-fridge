using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IRecipeService
    {
        public Task<ServiceResponse> CreateRecipe(RecipeDTO recipe, CancellationToken cancellationToken = default);

        public Task<ServiceResponse<RecipeDTO>> GetRecipeWithIngredient(IngredientDTO ingredient, CancellationToken cancellationToken = default);

    }
}
