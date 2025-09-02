using BlogAPI.ServicesLayer.Dtos;
using Business.ServicesLayer.Dtos;

namespace Business.ServicesLayer.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> GetCommentByIdAsync(int id);
        Task<IEnumerable<CommentDto>> GetAllCommentsByPostIdAsync(int blogPostId);
        Task<CommentDto> AddCommentAsync(CreateCommentDto commentDto);
        Task<CommentDto> UpdateCommentAsync(int id, EditCommentDto commentDto);
        Task DeleteCommentAsync(int id);
    }
}
