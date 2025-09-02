using BlogManager.Models;
using System.ComponentModel.DataAnnotations;

namespace Business.ServicesLayer.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }      
        public string Name { get; set; } 
        public List<BlogPost> BlogPosts { get; set; } 
    }
}
