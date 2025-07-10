using Domain.Entities;
using Domain.IReposotory;
using Infostructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infostructure.Repository
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }   


        public async Task AddUser(User user)
        {
              
          
                await _db.Users.AddAsync(user);

                await _db.SaveChangesAsync();
            
         
        }

        public async Task<User> GetUserByEmail(string email)
        {
           
              return await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        
        }

        public async Task<bool> IsUserExists(string username , string email)
        {
            return await _db.Users.AnyAsync(u => u.UserName == username ||  u.Email == email);
            
        
        }

        public async Task UpdateUserAsync(User user)
        {
            
                _db.Users.Update(user);

                await _db.SaveChangesAsync();
            
        }
    }
}
