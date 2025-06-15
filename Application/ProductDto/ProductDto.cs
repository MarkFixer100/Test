using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductDto
{
    public class ProductDtos
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal PricePerKg { get; set; }
        public double Weight { get; set; }

        public string StorageCondition { get; set; }
        public bool IsAvailable { get; set; }
        public Guid CategoryId { get; set; }

        
    }
}
