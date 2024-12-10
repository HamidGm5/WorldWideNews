using System.ComponentModel.DataAnnotations;

namespace WorldWideNews.API.Entities
{
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string NewsAgencyName { get; set; }
        public string ReporterName { get; set; }


        public Reporter Reporter { get; set; }
        public NewsAgency NewsAgency { get; set; }
        public CountryCategories CountryCategories { get; set; }
    }
}
