using System.ComponentModel.DataAnnotations;

namespace WorldWideNews.API.Entities
{
    public class Country
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<CountryCategories> CountryCategories { get; set; }
    }
}
