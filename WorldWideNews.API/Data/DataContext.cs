using Microsoft.EntityFrameworkCore;
using WorldWideNews.API.Entities;

namespace WorldWideNews.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) 
        {

        }

        public DbSet<News> News { get; set; }
        public DbSet<Reporter> Reporters { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CountryCategories> CountryCategories { get; set; }
        public DbSet<NewsAgency> NewsAgencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryCategories>()
                .HasKey(k => new { k.CategoryID, k.CountryID });

            modelBuilder.Entity<CountryCategories>()
                .HasOne(co => co.Country)
                .WithMany(c => c.CountryCategories)
                .HasForeignKey(c => c.CountryID);

            modelBuilder.Entity<CountryCategories>()
                .HasOne(ca => ca.Category)
                .WithMany(c => c.CountryCategories)
                .HasForeignKey(cf => cf.CategoryID);

        }
    }
}
