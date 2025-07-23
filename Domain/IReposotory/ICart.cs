using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IReposotory
{
    public interface ICart
    {
        Task Create(Cart entity);
        Task<Cart> GetCartByUserId(Guid id);
      //  void RemoveCartItem(CartItem existingItem, Cart cart);
        Task SaveChanges();
    }
}
