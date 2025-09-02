using BlogManager.Models;
using Business.ServicesLayer.Dtos;

namespace Business.ServicesLayer.Mappers
{
    public static class BlogPostMapper
    {
        public static BlogPost CreateBlogPostDtoToBlogPost(CreateBlogPostDto dto)
        {
            var blogPost = new BlogPost();
            blogPost.Headline = dto.Headline;
            blogPost.Body = dto.Body;
            blogPost.CategoryId = dto.CategoryId;
            return blogPost;
        }
        public static BlogPostDetailsDto BlogPostToBlogPostDetailsDto(BlogPost model)
        {
            var comments= model.Comments?.Select(c => CommentMapper.ModelToCommentDto(c)).ToList() ?? new List<CommentDto>();
            var blogPostDetailsDto = new BlogPostDetailsDto();
            blogPostDetailsDto.Id = model.Id;
            blogPostDetailsDto.Headline = model.Headline;
            blogPostDetailsDto.Body = model.Body;
            blogPostDetailsDto.PublishedDate = model.PublishedDate;
            blogPostDetailsDto.CategoryName = model.Category?.Name ?? "No Category";
            blogPostDetailsDto.AuthorName = model.Author?.Name ?? "Unknown Author";
            blogPostDetailsDto.Comments = comments;
            return blogPostDetailsDto;
        }
    }
}
