using Business.ServicesLayer.Dtos;

namespace Business.ServicesLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> AddCategoryAsync(CreateCategoryDto category);
        Task<CategoryDto> UpdateCategoryAsync(int id, CreateCategoryDto category);
        Task DeleteCategoryAsync(int id);
    }
}
