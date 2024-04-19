using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IngredientsController : AuthorizedController
    {
        private readonly IIngredientService _ingredientService;
        public IngredientsController(IUserService userService, IIngredientService ingredientService) : base(userService)
        {
            _ingredientService = ingredientService;
        }

        [Authorize] // You need to use this attribute to protect the route access, it will return a Forbidden status code if the JWT is not present or invalid, and also it will decode the JWT token.
        [HttpGet("{id:guid}")]

        public async Task<ActionResult<RequestResponse<IngredientDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
        {
            var response = await _ingredientService.GetIngredient(id);

            return response.Result != null ?
                this.FromServiceResponse(response) :
                this.ErrorMessageResult<IngredientDTO>();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<RequestResponse<PagedResponse<IngredientDTO>>>> GetAllIngredients([FromQuery] PaginationSearchQueryParams pagination)
        {
            var response = await _ingredientService.GetIngredients(pagination);

            return response.Result != null ?
                    this.FromServiceResponse(response) :
                    this.ErrorMessageResult<PagedResponse<IngredientDTO>>();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RequestResponse>> AddIngredient([FromBody] IngredientDTO ingredient)
        {
            var response = await _ingredientService.CreateIngredient(ingredient);

            return response != null ?
                this.FromServiceResponse(response) :
                this.ErrorMessageResult();
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<RequestResponse>> UpdateIngredient([FromRoute] Guid id, [FromBody] IngredientDTO ingredient)
        {
            var response = await _ingredientService.UpdateIngredient(id, ingredient);

            return response != null ?
                this.FromServiceResponse(response) :
                this.ErrorMessageResult();
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<RequestResponse>> DeleteIngredient([FromRoute] Guid id)
        {
            var response = await _ingredientService.DeleteIngredient(id);

            return response != null ?
                this.FromServiceResponse(response) :
                this.ErrorMessageResult();
        }
    }
}
