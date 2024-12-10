using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideNews.Models.Dtos
{
    public class NewsDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string NewsAgencyName { get; set; }
        public string ReporterName { get; set; }

    }
}
