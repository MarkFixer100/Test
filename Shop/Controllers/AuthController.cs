using Application.AuthDto;
using Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json.Serialization;
using System.Runtime.CompilerServices;

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

        public async Task<ActionResult> Register([FromBody]RegistreDto registreDto)
        {
            var response = await _authService.Register(registreDto);

            if (response is null)
            {
                return StatusCode(409 , "Пользователь Существует");
            }
            if(registreDto.Email == string.Empty || registreDto.Password == string.Empty || registreDto.Username == string.Empty || registreDto is null)
            {
                return StatusCode(422 ,"Неверно офрмленный запрос");
            }

            return Ok(response);
        }

        [HttpPost("login")]

        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var response = await _authService.Login(loginDto);

            if(response == null)
            {
                return Unauthorized();
            }

            

            return Ok(response);
        }

        [HttpPost("refresh-token")]

        public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody]RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshTokenAsync(request);

            if (result == null || request.RefreshToken == null)
            {
                return Unauthorized("Неверный  Токен Обновления");
            }

            return Ok(result);  
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok("admin");
        }

    }
}
