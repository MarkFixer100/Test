using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderDto
{
    public class CreateOrderDto
    {
        public Guid UserId { get; set; }
        public string? Address { get; set; }
        public string? deliveryMethod { get; set; }
        public string? PhoneNumber { get; set; }
        public string? paymentMethod { get; set; }
    }
}
