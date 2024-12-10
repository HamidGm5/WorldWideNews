namespace WorldWideNews.API.Entities
{
    public class CountryCategories
    {
        public int CountryID { get; set; }
        public int CategoryID { get; set; }

        public Country Country { get; set; }
        public Category Category { get; set; }
        public ICollection<News> News { get; set; }
    }
}
