using BlogManager.Models;

namespace BlogAPI.Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsByPostIdAsync(int blogPostId);
        Task<Comment?> GetCommentByIdAsync(int id);

        Task<Comment> AddCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
    }
}
