using BlogManager.Models;

namespace BlogManager.Repository.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
        Task<BlogPost> GetPostByIdAsync(int id);
        Task<BlogPost> AddBlogPostAsync(BlogPost blogPost);
        Task<BlogPost> UpdateBlogPostAsync(int id, BlogPost blogPost);
        Task DeletePostAsync(int id);
       
    }
}
