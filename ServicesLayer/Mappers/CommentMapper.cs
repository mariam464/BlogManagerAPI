using BlogManager.Models;
using Business.ServicesLayer.Dtos;

namespace Business.ServicesLayer.Mappers
{
    public static class CommentMapper
    {
        public static Comment CreateCommentDtoToModel(CreateCommentDto dto)
        {
            var comment = new Comment();
            comment.Content = dto.Content;
            comment.BlogPostId = dto.BlogPostId;
            return comment;
        }
        public static CommentDto ModelToCommentDto(Comment model)
        {
            var commentDto = new CommentDto();
            commentDto.Id = model.Id;
            commentDto.Content = model.Content;
            commentDto.DateCommented = model.DateCommented;
            commentDto.UserName = model.Author?.Name?? "Unknown";
            commentDto.BlogPostId = model.BlogPostId;
            return commentDto;
        }
    }
}
