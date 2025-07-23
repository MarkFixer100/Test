using Domain.Entities;
using Domain.IReposotory;
using Infostructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.Repository
{
    public class CartItemRepository: ICartItem
    {
        private readonly ApplicationDbContext _db;
        public CartItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(CartItem item)
        {
           _db.CartItems.Add(item);

            await _db.SaveChangesAsync();
            
        }

        public async Task remove(CartItem item)
        {
            _db.Remove(item);

            await _db.SaveChangesAsync();
        }
    }
}
