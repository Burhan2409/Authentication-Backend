using Microsoft.AspNetCore.Mvc;
using SCPL.Application.BusinessInterfaces;
using SCPL.Core.DBEntities;

namespace SCPL.API.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService) => _userService = userService;

        [HttpPost("LoginUser")]
        public IActionResult LoginUser([FromBody] UserTable user)
        {
            try
            {
                var token = _userService.LoginUserAsync(user, HttpContext);
                return Ok(new { Token = token });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
