using Domain.Entities;
using Domain.IReposotory;
using Infostructure.Data;
using Infostructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure.PerfumeRepository
{
    public class PerfumeRepository : Repository<Perfume>, IReposotoryPerfume
    {
        private readonly ApplicationDbContext _db;
        public PerfumeRepository(ApplicationDbContext db) : base(db) {
            {
                _db = db;
            }
        }

        public async Task<Perfume> UpdateAsync(Perfume entity)
        {
            entity.ReleaseDate = DateTime.Now;

            _db.Perfumes.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}