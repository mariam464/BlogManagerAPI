using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public List<BlogPost> BlogPosts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}
