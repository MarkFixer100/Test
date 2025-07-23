using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public User User { get; set; } = null!;
        public List<CartItem> Items { get; set; } = new();

        public decimal GetTotalPrice()
        {
            return Items.Sum(item => item.Product.PricePerKg * item.Quantity);
        }

    }
}
