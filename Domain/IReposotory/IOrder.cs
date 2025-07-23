using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IReposotory
{
    public interface IOrder
    {   
        public Task<List<Order>> GetOrdersByUserId(Guid userId);
        
        public Task<Order> GetOrderByUserId(Guid userId);
        public void Add(Order order);
        public Task saveChanges();

    }
}
