

using Domain.Entities;

namespace Domain.IReposotory
{
    public interface IUserRepository
    {
        Task<bool> IsUserExists(string username); // Проверка на уникальность
        Task<User> GetUserByEmail(string username); // Для аутентификации
        Task AddUser(User user); // Для регистрации
    }
}
