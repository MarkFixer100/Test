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
    public class StudentRepository : Repository<Student> , IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext db) : base(db)
        {
            
                _db = db;
            
        }

        public async Task<Student> UpdateAsync(Student entity)
        {
            entity.UpdatedDate = DateTime.Now;

            _db.Students.Update(entity);

            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
