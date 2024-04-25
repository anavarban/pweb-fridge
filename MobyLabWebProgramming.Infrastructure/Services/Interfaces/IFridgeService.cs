using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IFridgeService
    {
        public Task<ServiceResponse<FridgeDTO>> GetFridge(Guid userId, CancellationToken cancellationToken = default);
        public Task<ServiceResponse> CreateFridge(Guid userId, string name, CancellationToken cancellationToken = default);
        public Task<ServiceResponse> AddIngredientToFridge(Guid fridgeId, Guid ingredientId, CancellationToken cancellationToken = default);
        public Task<ServiceResponse> RemoveIngredientFromFridge(Guid fridgeId, Guid ingredientId, CancellationToken cancellationToken = default);
        public Task<ServiceResponse> UpdateIngredientInFridge(Guid fridgeId, Guid ingredientId, double quantity, CancellationToken cancellationToken = default);
        public Task<ServiceResponse> UpdateIngredientExpiryDate(Guid fridgeId, Guid ingredientId, DateTime expiryDate, CancellationToken cancellationToken = default);
    }
}
