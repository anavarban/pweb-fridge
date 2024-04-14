using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    internal interface IIngredientService
    {
        public Task<ServiceResponse<IngredientDTO>> GetIngredient(Guid id, CancellationToken cancellationToken = default);

        public Task<ServiceResponse<IEnumerable<IngredientDTO>>> GetIngredients(CancellationToken cancellationToken = default);

        public Task<ServiceResponse<IngredientDTO>> CreateIngredient(IngredientDTO ingredientDTO, CancellationToken cancellationToken = default);

        public Task<ServiceResponse<IngredientDTO>> UpdateIngredient(Guid id, IngredientDTO ingredientDTO, CancellationToken cancellationToken = default);

        public Task<ServiceResponse<IngredientDTO>> DeleteIngredient(Guid id, CancellationToken cancellationToken = default);
    }
}
