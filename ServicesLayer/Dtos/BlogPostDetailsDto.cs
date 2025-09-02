using BlogManager.Models;
using System.ComponentModel.DataAnnotations;

namespace Business.ServicesLayer.Dtos
{
    public class BlogPostDetailsDto
    {
        public int Id { get; set; }
        public string Headline { get; set; }

        public string Body { get; set; }
        public DateTime PublishedDate { get; set; }

        public string CategoryName { get; set; }
        public string AuthorName { get; set; } 

        public List<CommentDto> Comments { get; set; } 
    }
}
