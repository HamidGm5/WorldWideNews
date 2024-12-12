using Microsoft.EntityFrameworkCore;
using WorldWideNews.API.Data;
using WorldWideNews.API.Entities;
using WorldWideNews.API.Repository.Interfaces;

namespace WorldWideNews.API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddNewCategory(Category NewCategory)
        {
            try
            {
                await _context.Categories.AddAsync(NewCategory);
                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<bool> DeleteCategory(int ID)
        {
            try
            {
                var Category = await _context.Categories.FindAsync(ID);
                if (Category == null)
                {
                    return false;
                }
                _context.Categories.Remove(Category);
                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<ICollection<Category>> GetCategories()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<Category> GetCategoryByID(int CategoryID)
        {
            try
            {
                var category = await _context.Categories.FindAsync(CategoryID);
                if (category == null)
                {
                    return new Category();
                }
                return category;
            }
            catch
            {
                // log exception
                throw;
            }
        }

        public async Task<Category> GetNewsCategory(string CategoryName)
        {
            try
            {
                return await _context.Categories.Where(nc => nc.Name == CategoryName).FirstOrDefaultAsync();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateCategory(int CategoryID, Category NewCategory)
        {
            try
            {
                var FindCategory = await _context.Categories.FindAsync(CategoryID);
                if (FindCategory != null)
                {
                    FindCategory.Name = NewCategory.Name;
                    FindCategory.Icon = NewCategory.Icon;
                   
                    _context.Categories.Update(FindCategory);
                }

                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }
    }
}
