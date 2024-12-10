using System.ComponentModel.DataAnnotations;

namespace WorldWideNews.API.Entities
{
    public class Reporter
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Image { get; set; }

        public NewsAgency NewsAgency { get; set; }
    }
}
