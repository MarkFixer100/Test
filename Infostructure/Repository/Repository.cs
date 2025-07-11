﻿using Domain.Entities;
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
    public class Repository<T>:IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;

        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);

            await SaveAsync();
        }

        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }


    }
}
