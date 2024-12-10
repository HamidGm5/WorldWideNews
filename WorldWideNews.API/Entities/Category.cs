using System.ComponentModel.DataAnnotations;

namespace WorldWideNews.API.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Icon { get; set; }

        public ICollection<CountryCategories> CountryCategories { get; set; }
    }
}
