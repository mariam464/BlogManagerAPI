using Business.ServicesLayer.Dtos;

namespace Business.ServicesLayer.Services.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostDetailsDto>> GetAllBlogPostsAsync();
        Task<BlogPostDetailsDto> GetPostByIdAsync(int id);
        Task<BlogPostDetailsDto> AddBlogPostAsync(CreateBlogPostDto createBlogPostDto);
        Task<BlogPostDetailsDto> UpdateBlogPostAsync(int id, CreateBlogPostDto updateBlogPostDto);
        Task DeletePostAsync(int id);
    }
}
