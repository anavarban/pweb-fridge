using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IIngredientService
    {
        public Task<ServiceResponse<IngredientDTO>> GetIngredient(Guid id, CancellationToken cancellationToken = default);

        public Task<ServiceResponse<PagedResponse<IngredientDTO>>> GetIngredients(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

        public Task<ServiceResponse> CreateIngredient(IngredientDTO ingredientDTO, CancellationToken cancellationToken = default);

        public Task<ServiceResponse> UpdateIngredient(Guid id, IngredientDTO ingredientDTO, CancellationToken cancellationToken = default);

        public Task<ServiceResponse> DeleteIngredient(Guid id, CancellationToken cancellationToken = default);
    }
}
