using Microsoft.AspNetCore.Mvc;
using SCPL.Application.BusinessInterfaces;
using SCPL.Application.BusinessServices;
using SCPL.Core.DBEntities;

namespace SCPL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignUpController : Controller
    {
        private readonly IUserService _userService;

        public SignUpController(IUserService userService) => _userService = userService;

        [HttpPost("CreateUser")]
        public IActionResult SignUpUser(UserTable user)
        {
            try
            {
                var createdUser = _userService.SignUpUser(user);
                return Ok(createdUser);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }

}
