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
    public sealed class RecipeProjectionSpec : BaseSpec<RecipeProjectionSpec, Recipe, RecipeDTO>
    {
        protected override Expression<Func<Recipe, RecipeDTO>> Spec => e => new()
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            ImagePath = e.ImagePath,
            VideoPath = e.VideoPath,
            Servings = e.Servings,
            PrepTime = e.PrepTime,
            CookTime = e.CookTime,
            TotalTime = e.TotalTime,
            Ingredients = e.Ingredients.Select(i => new IngredientDTO
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                ImagePath = i.ImagePath,
                Quantity = i.Quantity,
                Unit = i.Unit,
                ExpiryDate = i.ExpiryDate
            }).ToList(),
            Instructions = e.Instructions
        };
        public RecipeProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
        {
        }

        public RecipeProjectionSpec(Guid id): base(id)
        { }

        public RecipeProjectionSpec(string? search)
        {
            search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

            if (search == null)
            {
                return;
            }

            var searchExpr = $"%{search.Replace(" ", "%")}%";

            Query.Where(e => EF.Functions.ILike(e.Name, searchExpr) || EF.Functions.ILike(e.Description, searchExpr);
        }

        public RecipeProjectionSpec(Ingredient ingredient)
        {
            Query.Where(e => e.Ingredients.Contains(ingredient));
        }

    }
}
