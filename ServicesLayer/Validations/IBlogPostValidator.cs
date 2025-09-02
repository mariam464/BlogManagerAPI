using Business.ServicesLayer.Dtos;

namespace Business.ServicesLayer.Validations
{
    public interface IBlogPostValidator
    {
       Task Validate(CreateBlogPostDto createBlogPostDto);
    }
}
