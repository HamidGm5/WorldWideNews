using Microsoft.EntityFrameworkCore;
using WorldWideNews.API.Data;
using WorldWideNews.API.Entities;
using WorldWideNews.API.Repository.Interfaces;

namespace WorldWideNews.API.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly DataContext _context;

        public NewsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNews(News newNews)
        {
            try
            {
                await _context.News.AddAsync(newNews);
                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<bool> DeleteNews(int ID)
        {
            try
            {
                var FindNews = await _context.News.FindAsync(ID);
                if (FindNews != null)
                {
                    _context.News.Remove(FindNews);
                    return await Save();
                }
                return false;
            }
            catch
            {
                // log exception
                throw;
            }
        }

        public async Task<ICollection<News>> GetAllNews()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<ICollection<News>> GetNewsByCategory(int CategoryID)
        {
            try
            {
                var FindNews = await _context.News.Where(nc => nc.CountryCategories.CategoryID == CategoryID).ToListAsync();
                return FindNews;
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<ICollection<News>> GetNewsByCountryFilter(int CategoryID, int CountryID)
        {
            try
            {
                if (CategoryID == 0)
                {
                    var CountryNews = await _context.News.Where(ne => ne.CountryCategories.CountryID == CountryID).ToListAsync();
                    return CountryNews;
                }
                else
                {
                    var News = await _context.News.Where(cn => cn.CountryCategories.CategoryID == CategoryID &&
                                                        cn.CountryCategories.CountryID == CountryID).ToListAsync();
                    return News;
                }
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<News> GetNewsByID(int ID)
        {
            try
            {
                var FindNews = await _context.News.FindAsync(ID);
                if (FindNews != null)
                {
                    return FindNews;
                }
                return new News();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<ICollection<News>> GetNewsByTitle(string Title)
        {
            try
            {
                var News = await _context.News.Where(n => n.Title.Contains(Title)).ToListAsync();
                return News;
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<bool> Save()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch
            {
                // log exception
                throw;
            }
        }

        public async Task<bool> UpdateNews(int NewsID, News newNews)
        {
            try
            {
                var FindNews = await _context.News.FindAsync(NewsID);
                if (FindNews != null)
                {
                    FindNews.Text = newNews.Text;
                    FindNews.Title = newNews.Title;
                    FindNews.Reporter = newNews.Reporter;
                    FindNews.ReporterName = newNews.ReporterName;
                    FindNews.Date = newNews.Date;

                    _context.News.Update(FindNews);
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
