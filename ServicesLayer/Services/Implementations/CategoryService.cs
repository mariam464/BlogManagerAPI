using BlogManager.Repository.Interfaces;
using Business.ServicesLayer.Dtos;
using Business.ServicesLayer.Exceptions;
using Business.ServicesLayer.Mappers;
using Business.ServicesLayer.Services.Interfaces;

namespace Business.ServicesLayer.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryDto> AddCategoryAsync(CreateCategoryDto categoryDto)
        {
           var category = await _categoryRepository.AddCategoryAsync(CategoryMapper.CreateCategoryDtoToModel(categoryDto));
            var categoryDetails = CategoryMapper.ModelToCategoryDto(category);
            return categoryDetails;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if(category == null)
            {
                throw new NotFoundException("Category not found");
            }
            await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            List<CategoryDto> categoriesDtos = new List<CategoryDto>();
            foreach(var item in categories)
            {
                categoriesDtos.Add(CategoryMapper.ModelToCategoryDto(item));
            }
            return categoriesDtos;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category != null)
            {
                return CategoryMapper.ModelToCategoryDto(category);
            } else
            {
                throw new NotFoundException("Category not found");
            }
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, CreateCategoryDto UpdatedCategory)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if(category == null)
            {
                throw new NotFoundException("Category not found");
            }
            category.Name = UpdatedCategory.Name;
            await _categoryRepository.UpdateCategoryAsync(id, category);
            return CategoryMapper.ModelToCategoryDto(category);
        }
    }
}
