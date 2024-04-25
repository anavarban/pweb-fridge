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
using System.Xml.Linq;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class FridgeService : IFridgeService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public FridgeService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse> AddIngredientToFridge(Guid fridgeId, Guid ingredientId, CancellationToken cancellationToken)
        {
            var fridge = await _repository.GetAsync<Fridge>(new FridgeProjectionSpec(fridgeId), cancellationToken);

            if (fridge == null)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Fridge not found!", ErrorCodes.EntityNotFound));
            }
            else
            {
                var ingredient = await _repository.GetAsync<Ingredient>(new IngredientProjectionSpec(ingredientId), cancellationToken);

                if (ingredient == null)
                {
                    return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Ingredient not found!", ErrorCodes.EntityNotFound));
                }

                var ingredients = fridge.Ingredients;
                if (ingredients == null)
                {
                    ingredients = new List<Ingredient>();
                }
                ingredients.Add(ingredient);
                fridge.Ingredients = ingredients;

                var result = await _repository.UpdateAsync(fridge, cancellationToken);

            }

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> CreateFridge(Guid userId, string name, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync<User>(new UserProjectionSpec(userId), cancellationToken);

            if (user == null)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "User not found!", ErrorCodes.EntityNotFound));
            }

            var fridge = new Fridge
            {
                Name = name,
                UserId = userId,
                User = user
            };

            user.Fridge = fridge;
            user.FridgeId = fridge.Id;

            await _repository.AddAsync(fridge, cancellationToken);
            await _repository.UpdateAsync(user, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> RemoveIngredientFromFridge(Guid fridgeId, Guid ingredientId, CancellationToken cancellationToken)
        {
            var fridge = await _repository.GetAsync<Fridge>(new FridgeProjectionSpec(fridgeId), cancellationToken);

            if (fridge == null)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Fridge not found!", ErrorCodes.EntityNotFound));
            }
            else
            {
                var ingredients = fridge.Ingredients;
                var ingredient = ingredients.FirstOrDefault(i => i.Id == ingredientId);

                if (ingredient == null)
                {
                    return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Ingredient not found!", ErrorCodes.EntityNotFound));
                }

                ingredients.Remove(ingredient);
                fridge.Ingredients = ingredients;

                var result = await _repository.UpdateAsync(fridge, cancellationToken);
            }

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateIngredientExpiryDate(Guid fridgeId, Guid ingredientId, DateTime expiryDate, CancellationToken cancellationToken)
        {
            var fridge = await _repository.GetAsync<Fridge>(new FridgeProjectionSpec(fridgeId), cancellationToken);

            if (fridge == null)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Fridge not found!", ErrorCodes.EntityNotFound));
            }
            else
            {
                var ingredients = fridge.Ingredients;
                var ingredient = ingredients.FirstOrDefault(i => i.Id == ingredientId);

                if (ingredient == null)
                {
                    return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Ingredient not found!", ErrorCodes.EntityNotFound));
                }

                ingredient.ExpiryDate = expiryDate;

                await _repository.UpdateAsync(fridge, cancellationToken);

                return ServiceResponse.ForSuccess();

            }
        }

        public async Task<ServiceResponse> UpdateIngredientInFridge(Guid fridgeId, Guid ingredientId, double quantity, CancellationToken cancellationToken)
        {

            var fridge = await _repository.GetAsync<Fridge>(new FridgeProjectionSpec(fridgeId), cancellationToken);

            if (fridge == null)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Fridge not found!", ErrorCodes.EntityNotFound));
            }
            else
            {
                var ingredients = fridge.Ingredients;
                var ingredient = ingredients.FirstOrDefault(i => i.Id == ingredientId);

                if (ingredient == null)
                {
                    return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Ingredient not found!", ErrorCodes.EntityNotFound));
                }

                ingredient.Quantity = quantity;

                await _repository.UpdateAsync(fridge, cancellationToken);

                return ServiceResponse.ForSuccess();

            }
        }

        public async Task<ServiceResponse<FridgeDTO>> GetFridge(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(new UserProjectionSpec(userId), cancellationToken);

            if (user == null)
            {
                return ServiceResponse<FridgeDTO>.FromError(new(HttpStatusCode.NotFound, "User not found!", ErrorCodes.EntityNotFound));
            }

            if (user.Fridge == null)
            {
                return ServiceResponse<FridgeDTO>.FromError(new(HttpStatusCode.NotFound, "Fridge not found!", ErrorCodes.EntityNotFound));
            }

            //var fridge = new FridgeDTO
            //{
            //    Id = user.Fridge.Id,
            //    Name = user.Fridge.Name,
            //    UserId = user.Id,
            //    Ingredients = user.Fridge.Ingredients
            //};

            return ServiceResponse<FridgeDTO>.ForSuccess(user.Fridge);
        }
    }
}
