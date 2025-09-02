using Business.ServicesLayer.Dtos;
using Business.ServicesLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
            return Ok(blogPosts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id > 0)
            {
                var blogPosts = await _blogPostService.GetPostByIdAsync(id);
                return Ok(blogPosts);
            }
            else
            {
                return BadRequest("Invalid id");
            }
        }
       
        [Authorize(Roles = "Author")]
        [HttpPost]
        public async Task<IActionResult> AddPost(CreateBlogPostDto dto)
        {
            if (ModelState.IsValid)
            {
                var blogPost = await _blogPostService.AddBlogPostAsync(dto);
                return Ok(blogPost);
            } else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Author")]
        [HttpPut]
        public async Task<IActionResult> UpdatePost(int id, CreateBlogPostDto dto)
        {
            if (id > 0)
            {
                if (ModelState.IsValid)
                {
                    var blogPost = await _blogPostService.UpdateBlogPostAsync(id, dto);
                    return Ok(blogPost);
                }
                else
                {
                    return BadRequest();
                }
            } else
            {
                return BadRequest("Invalid Id");
            }
        }

        [Authorize(Roles = "Author")]
        [HttpDelete]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id > 0)
            {
                await _blogPostService.DeletePostAsync(id);
                return NoContent();
            }
            else
            {
                return BadRequest("Invalid Id");
            }
        }
    }
}
