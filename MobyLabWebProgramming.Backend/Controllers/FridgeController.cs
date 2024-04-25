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
        [HttpGet]
        public async Task<ActionResult<RequestResponse<FridgeDTO>>> GetUserFridge()
        {
            var currentUser = await GetCurrentUser();
            if (currentUser.Result == null)
            {
                return this.ErrorMessageResult<FridgeDTO>(currentUser.Error);
            }

            var response = await _fridgeService.GetFridge(currentUser.Result.Id);

            return response.Result != null ?
                this.FromServiceResponse(response) :
                this.ErrorMessageResult<FridgeDTO>(response.Error);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RequestResponse>> AddIngredientToFridge([FromBody] Guid fridgeIngredient)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser.Result == null)
            {
                return this.ErrorMessageResult(currentUser.Error);
            }

            var response = await _fridgeService.GetFridge(currentUser.Result.Id);

            return response.Result != null ?
                this.FromServiceResponse(await _fridgeService.AddIngredientToFridge(response.Result.Id, fridgeIngredient)) :
                this.ErrorMessageResult();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RequestResponse>> CreateFridge([FromBody] string fridgeName)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser.Result == null)
            {
                return this.ErrorMessageResult(currentUser.Error);
            }

            return this.FromServiceResponse(await _fridgeService.CreateFridge(currentUser.Result.Id, fridgeName));
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<RequestResponse>> RemoveIngredientFromFridge([FromBody] Guid ingredientId)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser.Result == null)
            {
                return this.ErrorMessageResult(currentUser.Error);
            }

            var response = await _fridgeService.GetFridge(currentUser.Result.Id);

            return response.Result != null ?
                this.FromServiceResponse(await _fridgeService.RemoveIngredientFromFridge(response.Result.Id, ingredientId)) :
                this.ErrorMessageResult();
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<RequestResponse>> UpdateIngredientInFridge([FromBody] FridgeIngredientUpdateDTO ingredient)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser.Result == null)
            {
                return this.ErrorMessageResult(currentUser.Error);
            }

            var response = await _fridgeService.GetFridge(currentUser.Result.Id);

            return response.Result != null ?
                this.FromServiceResponse(await _fridgeService.UpdateIngredientInFridge(response.Result.Id, ingredient.Id, ingredient.Quantity)) :
                this.ErrorMessageResult();
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<RequestResponse>> UpdateIngredientExpiryDate([FromBody] FridgeIngredientUpdateDTO ingredient)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser.Result == null)
            {
                return this.ErrorMessageResult(currentUser.Error);
            }

            var response = await _fridgeService.GetFridge(currentUser.Result.Id);

            return response.Result != null ?
                this.FromServiceResponse(await _fridgeService.UpdateIngredientExpiryDate(response.Result.Id, ingredient.Id, ingredient.ExpiryDate)) :
                this.ErrorMessageResult();
        }
    }
}
