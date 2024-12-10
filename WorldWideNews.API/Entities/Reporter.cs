using System.ComponentModel.DataAnnotations;

namespace WorldWideNews.API.Entities
{
    public class Reporter
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string? Image { get; set; }

        public NewsAgency NewsAgency { get; set; }
        public ICollection<News> News { get; set; }
    }
}
