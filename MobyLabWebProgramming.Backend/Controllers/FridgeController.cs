using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FridgeController : AuthorizedController
    {
        private readonly IFridgeService _fridgeService;
        public FridgeController(IUserService userService, IFridgeService fridgeService) : base(userService)
        {
            _fridgeService = fridgeService;
        }

        [Authorize]
        [HttpGet("{user:guid}")]
        public async Task<ActionResult<RequestResponse<FridgeDTO>>> GetUserFridge([FromRoute] Guid userId)
        {
            var response = await _fridgeService.GetFridge(userId);

            return response.Result != null ?
                this.FromServiceResponse(response) :
                this.ErrorMessageResult<FridgeDTO>(response.Error);
        }

        [Authorize]
        [HttpPost("{user:guid}")]
        public async Task<ActionResult<RequestResponse>> AddIngredientToFridge([FromBody] Ingredient fridgeIngredient, [FromRoute] Guid userId)
        {
            var fridge = await _fridgeService.GetFridge(userId);

            return fridge.Result != null ?
                this.FromServiceResponse(await _fridgeService.AddIngredientToFridge(fridge.Result.Id, fridgeIngredient)) :
                this.ErrorMessageResult();
        }

        [Authorize]
        [HttpDelete("{user:guid}")]
        public async Task<ActionResult<RequestResponse>> RemoveIngredientFromFridge([FromBody] Ingredient fridgeIngredient, [FromRoute] Guid userId)
        {
            var fridge = await _fridgeService.GetFridge(userId);

            return fridge.Result != null ?
                this.FromServiceResponse(await _fridgeService.RemoveIngredientFromFridge(fridge.Result.Id, fridgeIngredient.Id)) :
                this.ErrorMessageResult();
        }

        [Authorize]
        [HttpPut("{user:guid}")]
        public async Task<ActionResult<RequestResponse>> UpdateIngredientInFridge([FromBody] FridgeIngredientUpdateDTO ingredient, [FromRoute] Guid userId)
        {
            var fridge = await _fridgeService.GetFridge(userId);

            return fridge.Result != null ?
                this.FromServiceResponse(await _fridgeService.UpdateIngredientInFridge(fridge.Result.Id, ingredient.Id, ingredient.Quantity)) :
                this.ErrorMessageResult();
        }

        [Authorize]
        [HttpPut("{user:guid}")]
        public async Task<ActionResult<RequestResponse>> UpdateIngredientExpiryDate([FromBody] FridgeIngredientUpdateDTO ingredient, [FromRoute] Guid userId)
        {
            var fridge = await _fridgeService.GetFridge(userId);

            return fridge.Result != null ?
                this.FromServiceResponse(await _fridgeService.UpdateIngredientExpiryDate(fridge.Result.Id, ingredient.Id, ingredient.ExpiryDate)) :
                this.ErrorMessageResult();
        }
    }
}
