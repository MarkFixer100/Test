using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        public string? Address { get; set; }
        public string? deliveryMethod { get; set; }
        public string? PhoneNumber { get; set; }
        public string? paymentMethod { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public List<OrderItem> Items { get; set; } = new();  
        public decimal GetTotalPrice()
        {
            return Items.Sum(item => item.UnitPrice * item.Quantity);
        }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
