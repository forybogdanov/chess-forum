using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Service.Mapping.Categories;
using GameWebsite.Service.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace GameWebsite.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly GameWebsiteDbContext gameWebsiteDbContext;

        public CategoryService(GameWebsiteDbContext gameWebsiteDbContext)
        {
            this.gameWebsiteDbContext = gameWebsiteDbContext;
        }

        public async Task<CategoryDto> CreateCategory(CategoryDto categoryDto)
        {
            Category category = categoryDto.ToEntity();
            category.CreatedBy = await this.gameWebsiteDbContext.Users.SingleOrDefaultAsync(user => user.Id == categoryDto.CreatedBy.Id);
            category.CreatedAt = DateTime.Now;
            await this.gameWebsiteDbContext.AddAsync(category);
            await this.gameWebsiteDbContext.SaveChangesAsync();
            // TODO: set posts
            return category.ToDto();
        }

        public async Task<CategoryDto> DeleteCategory(long id)
        {
            Category category = this.gameWebsiteDbContext.Categories.SingleOrDefault(category => category.Id == id);
            if (category == null)
            {
                // TODO: throw exception
                return null;
            }

            CategoryDto categoryDto = category.ToDto();
            this.gameWebsiteDbContext.Remove(category);
            await this.gameWebsiteDbContext.SaveChangesAsync();
            return categoryDto;
        }

        public IQueryable<CategoryDto> GetAllCategories()
        {
            return this.gameWebsiteDbContext.Categories
                .Include(category => category.CreatedBy)
                .Select(category => category.ToDto());
        }

        public async Task<CategoryDto> GetCategoryById(long id)
        {
            Category category = this.gameWebsiteDbContext.Categories
                .Include(category => category.CreatedBy)
                .SingleOrDefault(category => category.Id == id);
            if (category == null)
            {
                // TODO: throw exception
                return null;
            }
            return category.ToDto();
        }

        public async Task<CategoryDto> UpdateCategory(long id, CategoryDto categoryDto)
        {
            Category category = this.gameWebsiteDbContext.Categories.SingleOrDefault(category => category.Id == id);
            if (category == null)
            {
                // TODO: throw exception
                return null;
            }
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            this.gameWebsiteDbContext.Update(category);
            await this.gameWebsiteDbContext.SaveChangesAsync();
            // TODO: set posts
            return category.ToDto();    
        }
    }
}
