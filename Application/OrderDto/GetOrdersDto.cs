using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderDto
{
    public class GetOrdersDto
    {
        public string? Address { get; set; }
        public string? deliveryMethod { get; set; }
        public string? PhoneNumber { get; set; }
        public string? paymentMethod { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public List<OrderItem> Items { get; set; } = new();

    }
}
