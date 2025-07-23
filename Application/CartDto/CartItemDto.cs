using Application.ProductDto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartDto
{
    public class CartItemDto
    {

        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDtos product { get; set; }
        public int Quantity { get; set; }
    }
}
