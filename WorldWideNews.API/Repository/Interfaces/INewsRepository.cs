using WorldWideNews.API.Entities;

namespace WorldWideNews.API.Repository.Interfaces
{
    public interface INewsRepository
    {
        Task<ICollection<News>> GetAllNews();
        Task<ICollection<News>> GetNewsByCategory(int CategoryID);
        Task<ICollection<News>> GetNewsByCountryFilter(int CategoryID, int CountryID);
        Task<ICollection<News>> GetNewsByTitle(string Title);
        Task<bool> AddNews(News newNews);
        Task<bool> UpdateNews(News newNews);
        Task<bool> DeleteNews(int ID);
        Task<bool> Save();
    }
}
