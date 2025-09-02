using BlogAPI.ServicesLayer.Dtos;
using Business.ServicesLayer.Dtos;
using Business.ServicesLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCommentsForPost(int blogPostId)
        {
            if (blogPostId > 0)
            {
                var comments = await _commentService.GetAllCommentsByPostIdAsync(blogPostId);
                return Ok(comments);
            } else
            {
               return BadRequest("Invalid blog post id");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto dto)
        {
            if (ModelState.IsValid)
            {
                var comment = await _commentService.AddCommentAsync(dto);
                return Ok(comment);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditComment(int id, EditCommentDto dto)
        {
            if (id > 0)
            {
                if (ModelState.IsValid)
                {
                    var comment = await _commentService.UpdateCommentAsync(id, dto);
                    return Ok(comment);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Id");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id > 0)
            {
                await _commentService.DeleteCommentAsync(id);
                return NoContent();
            }
            else
            {
                return BadRequest("Invalid Id");
            }
        }
    }
}
