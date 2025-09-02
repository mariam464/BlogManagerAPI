using BlogManager.DBContext;
using BlogManager.Models;
using BlogManager.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Repository.Implementations
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;
        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BlogPost> AddBlogPostAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return await GetPostByIdAsync(blogPost.Id);
        }

        public async Task DeletePostAsync(int id)
        {
            var blogPostFromDb = await GetPostByIdAsync(id);
            if (blogPostFromDb != null)
            {
                _context.BlogPosts.Remove(blogPostFromDb);
               await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _context.BlogPosts
                .Include(bp => bp.Category)
                .Include(bp => bp.Author)
                .Include(bp => bp.Comments)
                .ToListAsync();
        }

        public async Task<BlogPost> GetPostByIdAsync(int id)
        {
            return await _context.BlogPosts
                .Include(bp => bp.Category)
                .Include(bp => bp.Author)
                .Include(bp => bp.Comments)
                .FirstOrDefaultAsync(bp => bp.Id == id);
        }

        public async Task<BlogPost> UpdateBlogPostAsync(int id,BlogPost blogPost)
        {
            var blogPostFromDb = await GetPostByIdAsync(id);
            blogPostFromDb.CategoryId = blogPost.CategoryId;
            blogPostFromDb.Body = blogPost.Body;
            blogPostFromDb.Headline = blogPost.Headline; 
            _context.BlogPosts.Update(blogPostFromDb);
            await _context.SaveChangesAsync();
            return blogPostFromDb;
           
        }
    }
}
