using Application.AuthDto;
using Application.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("api/AuthAPI")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) {

            _authService = authService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegistreDto registreDto)
        {
            var response = await _authService.Register(registreDto);

            return Ok(response);
        }

        [HttpPost("login")]

    public async Task<IActionResult> login(LoginDto loginDto)
        {
          var response = await  _authService.Login(loginDto);

            return Ok(response);
        }
    }
}
