using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects
{
    public class FridgeIngredientUpdateDTO
    {
        public Guid Id { get; set; }
        public double Quantity { get; set; } = 0;
        public DateTime ExpiryDate { get; set; } = DateTime.Today;
    }
}
