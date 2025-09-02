using BlogManager.Repository.Interfaces;
using Business.ServicesLayer.Dtos;
using Business.ServicesLayer.Exceptions;

namespace Business.ServicesLayer.Validations
{
    public class BlogPostValidator : IBlogPostValidator
    {
        private readonly ICategoryRepository _categoryRepository;
       
        public BlogPostValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task Validate(CreateBlogPostDto createBlogPostDto)
        {
            if (createBlogPostDto != null)
            {
                var errors = new List<string>();
                var category = await _categoryRepository.GetCategoryByIdAsync(createBlogPostDto.CategoryId);
                if (createBlogPostDto.Headline.Length >= 20 || createBlogPostDto.Headline.Length == 0)
                {
                    errors.Add("The headline length shouldn't be empty nor exceed 20 characters.");
                }
                if (createBlogPostDto.Body.Length <= 20)
                {
                    errors.Add("The body length shouldn't be less then 20 characters.");
                }
                if(category == null)
                {
                    errors.Add("Category doesn't exist");
                }
                if (errors.Any())
                {
                   throw new ValidationException(string.Join(" | ", errors));
                }
            }
            else
            {
                throw new ValidationException("The post can't be empty");
            }
        }
    }
}
