using Application.AuthDto;
using Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace Shop.Controllers
{
    [Route("api/AuthAPI")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {

            _authService = authService;
        }

        [HttpPost("register")]

        public async Task<ActionResult> Register(RegistreDto registreDto)
        {
            var response = await _authService.Register(registreDto);

            return Ok(response);
        }

        [HttpPost("login")]

        public async Task<ActionResult> login(LoginDto loginDto)
        {
            var response = await _authService.Login(loginDto);

            return Ok(response);
        }

        [HttpPost("refresh-token")]

        public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshTokenAsync(request);

            if (result == null || request.RefreshToken == null)
            {
                return Unauthorized("Неверный  Токен");
            }

            return Ok(result);  
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult adminOnly()
        {
            return Ok("admin");
        }

    }
}
