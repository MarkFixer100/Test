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

            if (user == null || !_passwordHasher.VerifyPassword(loginDto.Password , user.Password)) {
                 throw new UnauthorizedAccessException("Неверный логин или пароль");
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto { Token = token};
        }

        public async Task<AuthResponseDto> Register(RegistreDto registerDto)
        {
            if (await _userRepository.IsUserExists(registerDto.Username))
            {
                throw new ApplicationException("Пользователь уже существует");
            }
               

            var hashedPassword = _passwordHasher.Generate(registerDto.Password);

            var user = new User
            {
                UserName = registerDto.Username,

                Email = registerDto.Email,

                Password = hashedPassword
            };

            var token = _jwtService.GenerateToken(user);

            await _userRepository.AddUser(user);

            return new AuthResponseDto {Token = token , Username = user.UserName };


        }





    }
}
