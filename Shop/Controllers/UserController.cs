using Application.Use_Case;
using Application.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GetUser _userService;
        public UserController(GetUser userService) {
            
            _userService = userService;
        }

        [HttpGet]

        public async Task<ActionResult<UserDto>> GetUserById( Guid userId)
        {
            var user = await _userService.GetUserById(userId);

            if ( userId == Guid.Empty)
            {
                return BadRequest("Пустой ID");
            };

            if (user is null){
                return BadRequest("Не найден пользователь");
            }


            return user;
        }
    }
}
