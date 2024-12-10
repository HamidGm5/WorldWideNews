using System.ComponentModel.DataAnnotations;

namespace WorldWideNews.API.Entities
{
    public class NewsAgency
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Image { get; set; }

        public ICollection<News> News { get; set; }
        public ICollection<Reporter> Reporters { get; set; }
    }
}
