using WorldWideNews.API.Entities;

namespace WorldWideNews.API.Repository.Interfaces
{
    public interface INewsAgencyRepository
    {
        Task<ICollection<NewsAgency>> GetNewsAgencies();
        Task<NewsAgency> GetNewsAgencyByName(string Name);
        Task<bool> AddNewsAgency(NewsAgency newAgency);
        Task<bool> UpdateAgency(NewsAgency newAgency);
        Task<bool> DeleteAgency(int agencyId);
        Task<bool> Save();
    }
}
