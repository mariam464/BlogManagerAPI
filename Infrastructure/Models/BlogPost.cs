using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogManager.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        public string Headline { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; } = null!;

        public string AuthorId { get; set; } = string.Empty;
        [JsonIgnore]
        public ApplicationUser Author { get; set; } = null!;
        [JsonIgnore]
        public List<Comment> Comments { get; set; } = new();
    }
}