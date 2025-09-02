namespace Business.ServicesLayer.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateCommented { get; set; } 

        public int BlogPostId { get; set; }

        public string UserName { get; set; } 
    }
}
