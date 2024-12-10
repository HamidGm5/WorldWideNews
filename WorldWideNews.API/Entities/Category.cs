using System.ComponentModel.DataAnnotations;

namespace WorldWideNews.API.Entities
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Icon { get; set; }

        public ICollection<CountryCategories> CountryCategories { get; set; }
    }
}
