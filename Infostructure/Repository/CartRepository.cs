using Application.CartDto;
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



        public async Task Create(Cart entity)
        {
               _db.Cart.Add(entity);

            await SaveChanges();    

        }

        public async Task<Cart> GetCartByUserId(Guid id)
        {
            var cart = await _db.Cart.Include(i => i.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

                return cart;
         }


   

       
       

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }

       
    }
}
