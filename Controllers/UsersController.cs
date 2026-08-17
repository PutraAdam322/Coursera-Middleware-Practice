using Microsoft.AspNetCore.Mvc;

namespace WebApiPractice.Controllers
{
    [ApiController]
    [Route("api/[contoller]")]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService){
            _userService = userService;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<User>> Login(string username, string password)
        {
            var user = await _userService.LoginAsync(username, password);
            return Ok();
        }
    }
}