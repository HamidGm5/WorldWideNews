using Microsoft.EntityFrameworkCore;
using WorldWideNews.API.Data;
using WorldWideNews.API.Entities;
using WorldWideNews.API.Repository.Interfaces;

namespace WorldWideNews.API.Repository
{
    public class ReporterRepository : IReporterRepository
    {
        private readonly DataContext _context;

        public ReporterRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewReporter(Reporter NewReporter)
        {
            try
            {
                await _context.Reporters.AddAsync(NewReporter);
                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<bool> DeleteReporter(int ID)
        {
            try
            {
                var FindReporter = await _context.Reporters.FindAsync(ID);
                if (FindReporter != null)
                {
                    _context.Reporters.Remove(FindReporter);
                }
                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<ICollection<Reporter>> GetAgencyReporters(string AgencyName)
        {
            try
            {
                var AgencyReporters = await _context.Reporters.Where(ar => ar.NewsAgency.Name == AgencyName).ToListAsync();
                return AgencyReporters;
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<Reporter> GetReporterByID(int ReporterID)
        {
            try
            {
                var Reporter = await _context.Reporters.Where(r => r.ID == ReporterID).FirstOrDefaultAsync();
                if (Reporter != null)
                {
                    return Reporter;
                }
                return null;
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<ICollection<Reporter>> GetReporters()
        {
            return await _context.Reporters.ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateReporter(int ReporterID, Reporter NewReporter)
        {
            try
            {
                var FindReporter = await _context.Reporters.FindAsync(ReporterID);

                if (FindReporter != null)
                {
                    FindReporter.Name = NewReporter.Name;
                    FindReporter.NewsAgency = NewReporter.NewsAgency;
                    FindReporter.NewsAgencyName = NewReporter.NewsAgencyName;
                    FindReporter.Image = NewReporter.Image;

                    _context.Reporters.Update(FindReporter);
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
