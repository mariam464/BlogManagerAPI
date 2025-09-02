using BlogAPI.Repository.Interfaces;
using BlogAPI.ServicesLayer.Dtos;
using BlogManager.Models;
using BlogManager.Repository.Interfaces;
using Business.ServicesLayer.Dtos;
using Business.ServicesLayer.Exceptions;
using Business.ServicesLayer.Mappers;
using Business.ServicesLayer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Business.ServicesLayer.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentService(
                            ICommentRepository commentRepository, 
                            UserManager<ApplicationUser> userManager,
                            IHttpContextAccessor httpContextAccessor,
                            IBlogPostRepository blogPostRepository
                              )
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _blogPostRepository = blogPostRepository;
        }

        public async Task<CommentDto> AddCommentAsync(CreateCommentDto commentDto)
        {
            if (string.IsNullOrWhiteSpace(commentDto.Content))
                throw new ValidationException("Comment cannot be null");

            var blogPost = await _blogPostRepository.GetPostByIdAsync(commentDto.BlogPostId);
            if (blogPost == null)
                throw new NotFoundException("Blog post not found");

            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedException("User is not logged in");

            var comment = CommentMapper.CreateCommentDtoToModel(commentDto);
            comment.AuthorId = userId;
            comment.DateCommented = DateTime.UtcNow;

            await _commentRepository.AddCommentAsync(comment);

            
            var user = await _userManager.FindByIdAsync(userId);
            comment.Author = user;

            return CommentMapper.ModelToCommentDto(comment);
        }



        public Task<IEnumerable<CommentDto>> GetAllCommentsByPostIdAsync(int blogPostId)
        {
            var blogPost =  _blogPostRepository.GetPostByIdAsync(blogPostId);
            if (blogPost.Result == null)
            {
                throw new NotFoundException("Blog post not found");
            }
            var comments = _commentRepository.GetAllCommentsByPostIdAsync(blogPostId);
             var commentDtos = comments.Result.Select(c => CommentMapper.ModelToCommentDto(c));
             return Task.FromResult(commentDtos);
           
        }

        public async Task DeleteCommentAsync(int id)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedException("User is not logged in");

            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                throw new NotFoundException("Comment not found");

            if (comment.AuthorId != userId)
                throw new UnauthorizedException("You are not allowed to delete this comment");

            await _commentRepository.DeleteCommentAsync(id);
        }

        public async Task<CommentDto> UpdateCommentAsync(int id, EditCommentDto commentDto)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedException("User is not logged in");

            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                throw new NotFoundException("Comment not found");

            if (comment.AuthorId != userId)
                throw new UnauthorizedException("You are not allowed to edit this comment");

            comment.Content = commentDto.Content;
            await _commentRepository.UpdateCommentAsync(comment);

            return CommentMapper.ModelToCommentDto(comment);
        }

        public async Task<CommentDto> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                throw new NotFoundException("Comment not found");
            }
            return CommentMapper.ModelToCommentDto(comment);
        }
    }
}
