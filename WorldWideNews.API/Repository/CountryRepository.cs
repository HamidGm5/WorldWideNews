using Microsoft.EntityFrameworkCore;
using WorldWideNews.API.Data;
using WorldWideNews.API.Entities;
using WorldWideNews.API.Repository.Interfaces;

namespace WorldWideNews.API.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddNewCountry(Country country)
        {
            try
            {
                if (await _context.Countries.Where(fc => fc.Name.ToLower() == country.Name.ToLower())
                                            .FirstOrDefaultAsync() == null)
                {
                    await _context.Countries.AddAsync(country);
                }
                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<bool> DeleteCountry(string Name)
        {
            try
            {
                var FindCountry = await _context.Countries.Where(fc => fc.Name.ToLower() == Name.ToLower()).FirstOrDefaultAsync();
                if (FindCountry != null)
                {
                    _context.Countries.Remove(FindCountry);
                    return await Save();
                }
                return false;
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<ICollection<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<ICollection<Country>> GetCountriesByName(string Name)
        {
            try
            {
                var FindCountries = await _context.Countries.Where(cn =>
                    cn.Name.ToLower().StartsWith(Name.ToLower())).ToListAsync();

                return FindCountries;
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<Country> GetCountryByID(int ID)
        {
            try
            {
                var Country = await _context.Countries.FindAsync(ID);
                if (Country != null)
                {
                    return Country;
                }
                return new Country();
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

        public async Task<bool> UpdateCountry(Country country)
        {
            try
            {
                _context.Countries.Update(country);
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
