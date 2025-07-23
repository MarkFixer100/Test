using Domain.Entities;
using Domain.Enum;
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
    public class OrderRepository : IOrder
    {
      
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Order order)
        {
            _db.Order.Add(order);
        }

        public async Task<Order> GetOrderByUserId(Guid userId)
        {
            var order =  await _db.Order.FirstOrDefaultAsync(x => x.UserId == userId);

            return order;
            
        }

        public async Task<List<Order>> GetOrdersByUserId(Guid userId)
        {
           var order = await _db.Order.Where(u => u.UserId == userId).ToListAsync();

            return order;
        }

        
        public async Task saveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
