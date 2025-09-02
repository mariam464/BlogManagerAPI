using BlogManager.Models;
using BlogManager.Repository.Interfaces;
using Business.ServicesLayer.Dtos;
using Business.ServicesLayer.Exceptions;
using Business.ServicesLayer.Mappers;
using Business.ServicesLayer.Services.Interfaces;
using Business.ServicesLayer.Validations;
using Microsoft.AspNetCore.Identity;

namespace Business.ServicesLayer.Services.Implementations
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostValidator _blogPostValidator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogPostService(
            IBlogPostRepository blogPostRepository,
            IBlogPostValidator blogPostValidator,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
            _blogPostValidator = blogPostValidator;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BlogPostDetailsDto> AddBlogPostAsync(CreateBlogPostDto createBlogPostDto)
        {

            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (userId == null)
            {
                throw new UnauthorizedException("User is not logged in");
            }
            await _blogPostValidator.Validate(createBlogPostDto);
            var blogPost = BlogPostMapper.CreateBlogPostDtoToBlogPost(createBlogPostDto);
            blogPost.AuthorId = userId;
            await _blogPostRepository.AddBlogPostAsync(blogPost);
            return BlogPostMapper.BlogPostToBlogPostDetailsDto(blogPost);
        }

        public async Task DeletePostAsync(int id)
        {
            var blogPost = await _blogPostRepository.GetPostByIdAsync(id);
            if (blogPost == null)
                throw new NotFoundException("Post not found");

            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedException("User is not logged in");

            if (blogPost.AuthorId != userId)
                throw new UnauthorizedException("You are not allowed to delete this post");

            await _blogPostRepository.DeletePostAsync(id);
        }

        public async Task<IEnumerable<BlogPostDetailsDto>> GetAllBlogPostsAsync()
        {
            var blogPostsDetails = new List<BlogPostDetailsDto>();
            var blogPosts = await _blogPostRepository.GetAllBlogPostsAsync();
            foreach (var blogPost in blogPosts)
            {
                blogPostsDetails.Add(BlogPostMapper.BlogPostToBlogPostDetailsDto(blogPost));
            }
            return blogPostsDetails;
        }

        public async Task<BlogPostDetailsDto> GetPostByIdAsync(int id)
        {

            var blogPost = await _blogPostRepository.GetPostByIdAsync(id);
            if (blogPost == null)
            {
                throw new NotFoundException("Post not found");
            }
            else
            {
                return BlogPostMapper.BlogPostToBlogPostDetailsDto(blogPost);
            }

        }

        public async Task<BlogPostDetailsDto> UpdateBlogPostAsync(int id, CreateBlogPostDto updateBlogPostDto)
        {
            var blogPost = await _blogPostRepository.GetPostByIdAsync(id);
            if (blogPost == null)
                throw new NotFoundException("Post not found");

            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedException("User is not logged in");

            if (blogPost.AuthorId != userId)
                throw new UnauthorizedException("You are not allowed to update this post");

            await _blogPostValidator.Validate(updateBlogPostDto);
            blogPost.Headline = updateBlogPostDto.Headline;
            blogPost.Body = updateBlogPostDto.Body;
            blogPost.CategoryId = updateBlogPostDto.CategoryId;
            await _blogPostRepository.UpdateBlogPostAsync(id, blogPost);
            return BlogPostMapper.BlogPostToBlogPostDetailsDto(blogPost);
        }

    }
}
