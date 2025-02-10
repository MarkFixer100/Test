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
        public Task Create(Perfume entity)
        {
            throw new NotImplementedException();
        }

        public Task<Perfume> Get(Expression<Func<Perfume, bool>> filter = null, bool tracked = true)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Perfume>> GetAll(Expression<Func<Perfume, bool>> filter = null)
        {
            IQueryable<Perfume> query = _db.Perfumes;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public Task Remove(Perfume entity)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Update(Perfume entity)
        {
            throw new NotImplementedException();
        }
    }
}
