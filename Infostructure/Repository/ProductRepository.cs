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
    public class ProductRepository : Repository<Product>, IProducts
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {

            _db = db;
        }

        public async Task<PaginatedList<Product>> GetPaginatedSlice(int pageIndex, int pageSize)
        {
            var products = await _db.Products
                .OrderBy(p => p.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _db.Products.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginatedList<Product>(products, pageIndex, pageSize);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(Guid id)
        {
            List<Product> productsByCategory = await _db.Products.Where(p => p.CategoryId == id).ToListAsync();

            return productsByCategory;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            product.ProductionDate = DateTime.Now;

            _db.Products.Update(product);

            await _db.SaveChangesAsync();  
            return product;
        }
    }
}
