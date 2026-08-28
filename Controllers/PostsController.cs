using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApiPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetPost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound(new Response<string>(NotFound().StatusCode, "Post not found"));
            }
            return Ok(new Response<string>(Ok().StatusCode, "Post found", JsonSerializer.Serialize(post)));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreatePost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<string>(BadRequest().StatusCode, ModelState.Values.First().Errors.First().ErrorMessage));
            }
            await _postService.AddPostAsync(post);
            _postService.LogCreation($"Post created with ID: {post.Id}");
            return Ok(new Response<string>(Ok().StatusCode, "Post created successfully"));
        }
    }
}