using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartDto
{
    public class getCartDto
    {
        public Guid Id { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}
