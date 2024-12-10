using WorldWideNews.API.Entities;

namespace WorldWideNews.API.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategories();
        Task<Category> GetNewsCategory(string CategoryName);
        Task<bool> AddNewCategory(Category NewCategory);
        Task<bool> UpdateCategory(Category NewCategory);
        Task<bool> DeleteCategory(int ID);
        Task<bool> Save();
    }
}
