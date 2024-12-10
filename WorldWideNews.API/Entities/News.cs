using System.ComponentModel.DataAnnotations;

namespace WorldWideNews.API.Entities
{
    public class News
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public string ReporterName { get; set; }
        [Required]
        public string NewsAgencyName { get; set; }

        public Reporter Reporter { get; set; }
        public NewsAgency NewsAgency { get; set; }
        public CountryCategories CountryCategories { get; set; }
    }
}
