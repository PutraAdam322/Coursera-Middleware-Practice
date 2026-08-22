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

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] User user)
        {
            var usr = await _userService.LoginUserAsync(user.Username, user.Password);
            if(usr == null)
            {
                return Unauthorized("Wrong username or password");
            }
            return Ok(new Response<string>(200, "Login successful", usr.Username));
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            var usr = await _userService.RegisterUserAsync(user);
            if(usr == false)
            {
                return BadRequest("Username already exists");
            }
            return Ok(new Response<string>(200, "Registration successful"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if(user == null)
            {
                return NotFound(new Response<string>(404, "User not found"));
            }
            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllUser(int id)
        {
            var users = await _userService.GetAllUserAsync();
            Console.WriteLine($"Users count: {users.Count()}");
            if(users.Count() == 0)
            {
                return NotFound("Users Empty");
            }
            return Ok(users);
        }

    }
}