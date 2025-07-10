using Domain.Entities;
using Domain.IReposotory;
using Infostructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.Repository
{
    public class CartRepository : ICart
    {

        private readonly ApplicationDbContext _db;
        public CartRepository(ApplicationDbContext db)
        { 
            _db = db;
        }

        public void AddCartItem(CartItem cartItem, Cart cart)
        {
            cart.Items.Add(cartItem);
        }


        public async Task Create(Cart entity)
        {
               _db.Cart.Add(entity);

            await _db.SaveChangesAsync();

        }

        public async Task<Cart> GetCartByUserId(Guid id)
        {
            var cart = await _db.Cart.Include(i => i.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(x => x.UserId == id);

                return cart;
        }


   

       
        public  void RemoveCartItem(CartItem cartItem, Cart cart)
        {
            
            cart.Items.Remove(cartItem);
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }

       
    }
}
