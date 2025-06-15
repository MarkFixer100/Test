using Application.AuthDto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<AuthResponseDto> Register(RegistreDto registerDto);
        
        Task<AuthResponseDto> RefreshTokenAsync (RefreshTokenRequestDto request);

    }
}
