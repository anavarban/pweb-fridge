using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class IngredientProjectionSpec : BaseSpec<IngredientProjectionSpec, Ingredient, IngredientDTO>
    {
        protected override Expression<Func<Ingredient, IngredientDTO>> Spec => ingredient => new()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Description = ingredient.Description,
            ImagePath = ingredient.ImagePath,
            Quantity = ingredient.Quantity,
            Unit = ingredient.Unit,
            ExpiryDate = ingredient.ExpiryDate
        };

        public IngredientProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
        {
        }

        public IngredientProjectionSpec(Guid id) : base(id)
        {
        }

        public IngredientProjectionSpec(string? search)
        {
            search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

            if (search == null)
            {
                return;
            }

            var searchExpr = $"%{search.Replace(" ", "%")}%";

            Query.Where(e => EF.Functions.ILike(e.Name, searchExpr) || EF.Functions.ILike(e.Description, searchExpr);
        }

        public IngredientProjectionSpec(Recipe recipe)
        {
            Query.Where(e => e.Recipes.Contains(recipe));
        }
    }
}
