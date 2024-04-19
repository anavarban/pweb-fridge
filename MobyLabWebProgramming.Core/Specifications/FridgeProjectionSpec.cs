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
    public sealed class FridgeProjectionSpec : BaseSpec<FridgeProjectionSpec, Fridge, FridgeDTO>
    {
        protected override Expression<Func<Fridge, FridgeDTO>> Spec => e => new()
        {
            Id = e.Id,
            Name = e.Name,
            Ingredients = e.Ingredients
        };

        public FridgeProjectionSpec(Guid id) : base(id)
        {
        }
    }

}
