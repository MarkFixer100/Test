

using Domain.Entities;

namespace Domain.IReposotory
{
    public interface IUserRepository
    {
        Task<bool> IsUserExists(string username); 
        Task<User> GetUserByEmail(string username); 
        Task AddUser(User user); 
    }
}
