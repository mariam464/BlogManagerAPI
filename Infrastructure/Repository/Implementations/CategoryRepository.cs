using BlogManager.DBContext;
using BlogManager.Models;
using BlogManager.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Repository.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var categoryPostFromDb = await GetCategoryByIdAsync(id);
            if (categoryPostFromDb != null)
            {
                _context.Categories.Remove(categoryPostFromDb);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Include(c => c.BlogPosts)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
               .Include(c => c.BlogPosts)
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category updatedCategory)
        {
            var categoryFromDb = await GetCategoryByIdAsync(id);
            categoryFromDb.Name = updatedCategory.Name;
            _context.Categories.Update(categoryFromDb);
            await _context.SaveChangesAsync();
            return categoryFromDb;
        }
    }
}
