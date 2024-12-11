using WorldWideNews.API.Entities;

namespace WorldWideNews.API.Repository.Interfaces
{
    public interface ICountryRepository
    {
        Task<ICollection<Country>> GetCountries();
        Task<ICollection<Country>> GetCountriesByName(string Name); // for filter by countries name
        Task<Country> GetCountryByID(int ID);
        Task<bool> AddNewCountry(Country country);
        Task<bool> UpdateCountry(Country country);
        Task<bool> DeleteCountry(string Name);
        Task<bool> Save();
    }
}
