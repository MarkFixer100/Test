using Domain.Entities;
using Domain.IReposotory;
using Infostructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.Repository
{
    public class Repository : IReposotoryPerfume
    {
        private readonly ApplicationDbContext _db;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }
     

        public async Task<Perfume> GetAsync(Expression<Func<Perfume, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Perfume> query = _db.Perfumes;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);    
            }

            return await query.FirstOrDefaultAsync(); 
        }

        public async Task<List<Perfume>> GetAllAsync(Expression<Func<Perfume, bool>> filter = null)
        {
            IQueryable<Perfume> query = _db.Perfumes;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task CreateAsync(Perfume entity)
        {
            await _db.Perfumes.AddAsync(entity);

            await SaveAsync();
        }

        public async Task Remove(Perfume entity)
        {
             _db.Perfumes.Remove(entity);

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

 
    }
}
