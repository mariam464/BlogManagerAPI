using BlogManager.Models;
using Business.ServicesLayer.Dtos;

namespace Business.ServicesLayer.Mappers
{
    public static class CategoryMapper
    {
        public static Category CreateCategoryDtoToModel(CreateCategoryDto dto)
        {
            var category = new Category();
            category.Name = dto.Name;
            return category;
        }
        public static CategoryDto ModelToCategoryDto(Category model)
        {
            var categoryDto = new CategoryDto();
            categoryDto.Id = model.Id;
            categoryDto.Name= model.Name;
            categoryDto.BlogPosts = model.BlogPosts.ToList();
            return categoryDto;
        }
    }
}
