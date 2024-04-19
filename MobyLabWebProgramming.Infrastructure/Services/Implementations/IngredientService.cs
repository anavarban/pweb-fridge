using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
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
    public class IngredientService: IIngredientService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public IngredientService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse> CreateIngredient(IngredientDTO ingredientDTO, CancellationToken cancellationToken)
        {
            var result = await _repository.AddAsync(new Ingredient
            {
                Name = ingredientDTO.Name,
                ExpiryDate = ingredientDTO.ExpiryDate,
                Quantity = ingredientDTO.Quantity,
                Unit = ingredientDTO.Unit,
                Description = ingredientDTO.Description,
                ImagePath = ingredientDTO.ImagePath,
            }, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteIngredient(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteAsync<Ingredient>(id, cancellationToken);

            return result != 0 ? ServiceResponse.ForSuccess() : ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Ingredient not found!", ErrorCodes.EntityNotFound));
        }

        public async Task<ServiceResponse<IngredientDTO>> GetIngredient(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync<Ingredient>(id, cancellationToken);

            return result != null ?
                ServiceResponse<IngredientDTO>.ForSuccess(new IngredientDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    ExpiryDate = result.ExpiryDate,
                    Quantity = result.Quantity,
                    Unit = result.Unit,
                    Description = result.Description,
                    ImagePath = result.ImagePath,
                    Recipes = result.Recipes
                }) :
                ServiceResponse<IngredientDTO>.FromError(new(HttpStatusCode.NotFound, "Ingredient not found!", ErrorCodes.EntityNotFound));
        }

        public async Task<ServiceResponse<PagedResponse<IngredientDTO>>> GetIngredients(PaginationSearchQueryParams pagination, CancellationToken cancellationToken)
        {
            var result = await _repository.PageAsync(pagination, new IngredientProjectionSpec(pagination.Search), cancellationToken);

            return ServiceResponse<PagedResponse<IngredientDTO>>.ForSuccess(result);
        }

        public async Task<ServiceResponse> UpdateIngredient(Guid id, IngredientDTO ingredientDTO, CancellationToken cancellationToken)
        {
            var result = await _repository.UpdateAsync(new Ingredient
            {
                Id = id,
                Name = ingredientDTO.Name,
                ExpiryDate = ingredientDTO.ExpiryDate,
                Quantity = ingredientDTO.Quantity,
                Unit = ingredientDTO.Unit,
                Description = ingredientDTO.Description,
                ImagePath = ingredientDTO.ImagePath,
            }, cancellationToken);

            return result != null ? ServiceResponse.ForSuccess() : ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Ingredient not found!", ErrorCodes.EntityNotFound));
        }
    }
}
