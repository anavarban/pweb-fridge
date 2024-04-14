using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
        public string VideoPath { get; set; } = default!;
        public int Servings { get; set; } = 0;
        public int PrepTime { get; set; } = 0;
        public int CookTime { get; set; } = 0;
        public int TotalTime { get; set; } = 0;
        public ICollection<IngredientDTO> Ingredients { get; set; } = new List<IngredientDTO>();
        public string Instructions { get; set; } = default!;
    }
}
