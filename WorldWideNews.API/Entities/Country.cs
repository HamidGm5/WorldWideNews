﻿using System.ComponentModel.DataAnnotations;

namespace WorldWideNews.API.Entities
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<CountryCategories> CountryCategories { get; set; }
    }
}
