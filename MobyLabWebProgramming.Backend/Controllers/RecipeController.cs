using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Net;

namespace MobyLabWebProgramming.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RecipeController : AuthorizedController
    {
        private readonly IRecipeService _recipeService;
        private readonly IIngredientService _ingredientService;

        public RecipeController(IRecipeService recipeService, IIngredientService ingredientService, IUserService userService) : base(userService)
        {
            _recipeService = recipeService;
            _ingredientService = ingredientService;
        }

        [HttpGet("{inredientId:guid}")]
        public async Task<ActionResult<RequestResponse<RecipeDTO>>> GetRecipeByIngredientId([FromRoute] Guid inredientId)
        {
            var response = await _ingredientService.GetIngredient(inredientId);

            return response.Result != null ?
                this.FromServiceResponse(await _recipeService.GetRecipeWithIngredient(response.Result)) :
                this.ErrorMessageResult<RecipeDTO>(response.Error);
        }

        [HttpPost]
        public async Task<ActionResult<RequestResponse>> AddRecipe([FromBody] RecipeDTO recipe)
        {
            var response = await _recipeService.CreateRecipe(recipe);

            return response != null ?
                this.FromServiceResponse(response) :
                this.ErrorMessageResult();
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<RequestResponse>> DeleteRecipe([FromRoute] Guid id)
        {
            var user = await GetCurrentUser();

            if (user.Result == null)
            {
                return this.ErrorMessageResult(user.Error);
            }

            var response = await _recipeService.DeleteRecipe(id, user.Result);

            return response != null ?
                this.FromServiceResponse(response) :
                this.ErrorMessageResult();
        }
    }
}
