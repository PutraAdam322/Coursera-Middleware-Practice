using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserDTO user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new Response<string>(BadRequest().StatusCode, ModelState.Values.First().Errors.First().ErrorMessage));
            }
            var usr = await _userService.LoginUserAsync(user);
            if(usr == null)
            {
                return Unauthorized(new Response<User>(Unauthorized().StatusCode, "Invalid username or password"));
            }
            return Ok(new Response<User>(200, "Login successful", usr));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] UserDTO user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new Response<string>(BadRequest().StatusCode, ModelState.Values.First().Errors.First().ErrorMessage));
            }
            var usr = await _userService.RegisterUserAsync(user);
            if(usr == false)
            {
                return BadRequest(new Response<string>(BadRequest().StatusCode, "Username already exists"));
            }
            return Ok(new Response<string>(Ok().StatusCode, "Registration successful"));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if(user == null)
            {
                return Ok(new Response<string>(Ok().StatusCode, "User not found"));
            }
            return Ok(new Response<string>(Ok().StatusCode, "User found", JsonSerializer.Serialize(user)));
        }

        [HttpGet("all")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> GetAllUser(int id)
        {
            var users = await _userService.GetAllUserAsync();
            Console.WriteLine($"Users count: {users.Count()}");
            if(users.Count() == 0)
            {
                return NoContent();
            }
            return Ok(new Response<IEnumerable<User>>(Ok().StatusCode, "Users found", users));
        }

    }
}