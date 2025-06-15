
using Domain.Entities;

namespace Domain.IReposotory
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsUserExists(string username, string email);
        Task<User> GetUserByEmail(string username); 
        Task AddUser(User user);

        Task UpdateUserAsync(User user);

    }
}
