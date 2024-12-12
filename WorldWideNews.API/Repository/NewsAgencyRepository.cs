using Microsoft.EntityFrameworkCore;
using WorldWideNews.API.Data;
using WorldWideNews.API.Entities;
using WorldWideNews.API.Repository.Interfaces;

namespace WorldWideNews.API.Repository
{
    public class NewsAgencyRepository : INewsAgencyRepository
    {
        private readonly DataContext _context;

        public NewsAgencyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewsAgency(NewsAgency newAgency)
        {
            try
            {
                if (!await _context.NewsAgencies.AnyAsync(fn => fn.Name.ToLower() == newAgency.Name.ToLower()))
                {
                    await _context.NewsAgencies.AddAsync(newAgency);
                }
                return await Save();
            }
            catch
            {
                //Log Exception
                throw;
            }
        }

        public async Task<bool> DeleteNewsAgency(int agencyId)
        {
            try
            {
                var NewsAgencies = await _context.NewsAgencies.FindAsync(agencyId);
                if (NewsAgencies != null)
                {
                    _context.NewsAgencies.Remove(NewsAgencies);
                }
                return await Save();
            }
            catch
            {
                // Log Exception
                throw;
            }
        }

        public async Task<ICollection<NewsAgency>> GetNewsAgencies()
        {
            try
            {
                return await _context.NewsAgencies.ToListAsync();
            }
            catch
            {
                //Log Exception
                throw;
            }
        }

        public async Task<NewsAgency> GetNewsAgencyByID(int NewsAgencyID)
        {
            try
            {
                var NewsAgency = await _context.NewsAgencies.FindAsync(NewsAgencyID);
                if (NewsAgency != null)
                {
                    return NewsAgency;
                }
                return new NewsAgency();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<NewsAgency> GetNewsAgencyByName(string Name)
        {
            try
            {
                var Agency = await _context.NewsAgencies.FirstOrDefaultAsync(x => x.Name == Name);
                if (Agency != null)
                {
                    return Agency;
                }
                return new NewsAgency();
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

        public async Task<bool> UpdateNewsAgency(int NewsAgencyID, NewsAgency newAgency)
        {
            try
            {
                var FindAgency = await _context.NewsAgencies.FindAsync(NewsAgencyID);
                if (FindAgency != null)
                {
                    FindAgency.Name = newAgency.Name;
                    FindAgency.Image = newAgency.Image;
                    
                    _context.NewsAgencies.Update(FindAgency);
                }

                return await Save();
            }
            catch
            {
                //Log Exception
                throw;
            }
        }
    }
}
