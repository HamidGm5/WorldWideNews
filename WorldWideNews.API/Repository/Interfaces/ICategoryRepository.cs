namespace WorldWideNews.API.Repository.Interfaces
{
    public class ICategoryRepository
    {
        Task<ICollection<NewsCategory>> GetCategories();
        Task<NewsCategory> GetNewsCategory(string CategoryName);
        Task<bool> AddNewCategory(NewsCategory NewCategory);
        Task<bool> UpdateCategory(NewsCategory NewCategory);
        Task<bool> DeleteCategory(int ID);
        Task<bool> Save();
    }
}
