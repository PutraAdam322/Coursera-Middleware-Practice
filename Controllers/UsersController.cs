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

        [HttpPost("register")]
        public async Task<ActionResult> Login(string username, string password)
        {
            var user = await _userService.LoginUserAsync(username, password);
            if(user == null)
            {
                return Unauthorized("Wrong username or password");
            }
            return Created($"/api/{user.Id}", user);
        }
    }
}