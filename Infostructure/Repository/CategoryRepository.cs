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
    public class CategoryRepository:Repository<Category> , ICategory
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {

            _db = db;
        }
    }
}
