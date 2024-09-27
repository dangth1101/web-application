
using blog.DTOs;
using blog.Service;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("article")]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _blogService.GetArticles();
            return Ok(articles);
        }

        [HttpGet("comment")]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _blogService.GetComments();
            return Ok(comments);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _blogService.GetUsers();
            return Ok(users);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _blogService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User data is null.");
            }

            var createdUserId = await _blogService.CreateUser(userDTO);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUserId }, userDTO);
        }
    }
}