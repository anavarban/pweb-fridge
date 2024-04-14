using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Fridge: BaseEntity
    {   
        public Guid UserId { get; set; }

        public User User { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ICollection<Ingredient> Ingredients { get; set; } = default!;
    }
}
