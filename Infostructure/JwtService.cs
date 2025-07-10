
using Application.interfaces;
using Domain.Entities;
using Domain.IReposotory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infostructure
{
    public class JwtService : IJwtService 
    {
        private readonly JwtOptions _options;

        private readonly IUserRepository _userRepository;
        public JwtService(IOptions<JwtOptions> options , IUserRepository userRepository) { 
            
            _options = options.Value;
            _userRepository = userRepository;
        }

        public async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpirytime = DateTime.UtcNow.AddDays(7);

            await _userRepository.UpdateUserAsync(user);

            return refreshToken;
        }

        public async Task<User> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            User user = await _userRepository.GetAsync(u => u.Id == userId);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpirytime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);

        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name , user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role, user.Role),
            };


            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                       issuer: _options.Issuer,            
                       audience: _options.Audience,         
                       claims: claims,
                       expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes),
                       signingCredentials: signingCredentials
    );
            
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;

        }




    }


  
}
