using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);

        string GenerateRefreshToken();

        Task<string> GenerateAndSaveRefreshTokenAsync (User user);

        Task <User> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
    }
}
