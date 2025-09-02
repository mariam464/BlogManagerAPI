using System.ComponentModel.DataAnnotations;

namespace BlogManager.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<BlogPost> BlogPosts { get; set; } = new();
    }

}