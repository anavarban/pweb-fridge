using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects
{
    public class IngredientDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
        public double Quantity { get; set; } = 0;
        public string Unit { get; set; } = default!;
        public DateTime ExpiryDate { get; set; } = DateTime.Today;

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
