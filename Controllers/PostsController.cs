using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text.Json;

namespace WebApiPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ITokenService _tokenService;

        public PostsController(IPostService postService, ITokenService tokenService)
        {
            _postService = postService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            if (posts == null || !posts.Any())
            {
                return NotFound(new Response<string>(NoContent().StatusCode, "No posts found"));
            }
            return Ok(new Response<IEnumerable<Post>>(Ok().StatusCode, "Posts retrieved successfully", posts));
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
            return Ok(new Response<Post>(Ok().StatusCode, "Post found", post));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreatePost([FromBody] PostDTO pst)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<string>(BadRequest().StatusCode, ModelState.Values.First().Errors.First().ErrorMessage));
            }
            string? stringToken = await HttpContext.GetTokenAsync("access_token");
            int userId = await _tokenService.GetUserIdFromToken(stringToken);
            var post = new Post(pst.Title, pst.Content, userId);
            await _postService.AddPostAsync(post, userId);
            _postService.LogCreation($"Post created with ID: {post.Id}");
            return Ok(new Response<string>(Ok().StatusCode, "Post created successfully"));
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdatePost(int id, [FromBody] PostDTO post)
        {
            string? stringToken = await HttpContext.GetTokenAsync("access_token");
            int userId = await _tokenService.GetUserIdFromToken(stringToken);
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<string>(BadRequest().StatusCode, ModelState.Values.First().Errors.First().ErrorMessage));
            }

            if(post.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this post.");
            }

            try 
            {
                await _postService.UpdatePostAsync(id, post);
                return Ok(new Response<string>(Ok().StatusCode, "Post updated successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new Response<string>(NotFound().StatusCode, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeletePost(int id)
        {
            string? stringToken = await HttpContext.GetTokenAsync("access_token");
            int userId = await _tokenService.GetUserIdFromToken(stringToken);

            var post = await _postService.GetPostByIdAsync(id);

            if(post.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this post.");
            }

            try 
            {
                await _postService.DeletePostAsync(id, userId);
                return Ok(new Response<string>(Ok().StatusCode, "Post deleted successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new Response<string>(NotFound().StatusCode, ex.Message));
            }
        }
    }
}