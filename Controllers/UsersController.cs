using Microsoft.AspNetCore.Mvc;

namespace WebApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if(user == null)
            {
                return NotFound("404 Not Found");
            }
            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllUser(int id)
        {
            var users = await _userService.GetAllUserAsync();
            if(users == null)
            {
                return NotFound("404 Not Found");
            }
            return Ok(users);
        }

    }
}