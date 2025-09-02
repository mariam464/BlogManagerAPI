namespace Business.ServicesLayer.Dtos
{
    public class CreateBlogPostDto
    {
        public string Headline { get; set; }

        public string Body { get; set; }
        public int CategoryId { get; set; }
        
    }
}
