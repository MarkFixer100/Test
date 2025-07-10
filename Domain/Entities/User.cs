using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;    
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = Domain.Enum.Role.User;
        public string? RefreshToken { get; set; }
        public Guid CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart? Cart { get; set; }
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public List<Order>? Orders { get; set; }
        public DateTime? RefreshTokenExpirytime { get; set; }
    }
}
