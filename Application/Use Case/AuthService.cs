using Application.AuthDto;
using Application.interfaces;
using Domain.Entities;
using Domain.IReposotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Case
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _userRepository;

        private readonly IPasswordHasher _passwordHasher;

        private readonly IJwtService _jwtService;

       public  AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher , IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);

            if (user == null || !_passwordHasher.VerifyPassword(loginDto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Неверный логин или пароль");
            }

            var accessToken = _jwtService.GenerateToken(user);

            var refreshToken = await _jwtService.GenerateAndSaveRefreshTokenAsync(user);

            return new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var user = await _jwtService.ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);

            if (user == null)
            {
                return null;
            }

            var accessToken = _jwtService.GenerateToken(user);

            var refreshToken = await _jwtService.GenerateAndSaveRefreshTokenAsync(user);

            return new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDto> Register(RegistreDto registerDto)
        {
            if (await _userRepository.IsUserExists(registerDto.Username , registerDto.Email))
            {
                
                return null;    
            }
            
            

            var hashedPassword = _passwordHasher.Generate(registerDto.Password);

           

            var refreshToken = _jwtService.GenerateRefreshToken();
          

            var user = new User
            {
                UserName = registerDto.Username,

                Email = registerDto.Email,

                Password = hashedPassword,

                RefreshToken = refreshToken,

                RefreshTokenExpirytime = DateTime.UtcNow.AddDays(7)


            };


            var token = _jwtService.GenerateToken(user);

            await _userRepository.AddUser(user);

            return new AuthResponseDto {Token = token , RefreshToken = refreshToken};


        }

     
    }
}
