using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; } = null!;
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
