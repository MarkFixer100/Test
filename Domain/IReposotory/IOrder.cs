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
        public void ChangeStatusOrder(Order order , OrderStatus status);

        public Task<Order> GetOrderById(Guid userId);

        public void ClearCart(Cart cart);


    }
}
