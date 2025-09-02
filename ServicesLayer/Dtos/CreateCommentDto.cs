namespace Business.ServicesLayer.Dtos
{
    public class CreateCommentDto
    {
        public int BlogPostId { get; set; }
        public string Content { get; set; }
    }
}
