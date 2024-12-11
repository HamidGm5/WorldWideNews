using WorldWideNews.API.Entities;

namespace WorldWideNews.API.Repository.Interfaces
{
    public interface INewsAgencyRepository
    {
        Task<ICollection<NewsAgency>> GetNewsAgencies();
        Task<NewsAgency> GetNewsAgencyByName(string Name);
        Task<NewsAgency> GetNewsAgencyByID(int NewsAgencyID);
        Task<bool> AddNewsAgency(NewsAgency newAgency);
        Task<bool> UpdateNewsAgency(NewsAgency newAgency);
        Task<bool> DeleteNewsAgency(int agencyId);
        Task<bool> Save();
    }
}
