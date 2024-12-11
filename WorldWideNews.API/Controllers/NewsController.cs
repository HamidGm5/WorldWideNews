using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorldWideNews.API.Repository.Interfaces;
using WorldWideNews.API.Entities;
using WorldWideNews.Models.Dtos;

namespace WorldWideNews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly INewsRepository _repository;
        private readonly IReporterRepository _reporterRepository;
        private readonly INewsAgencyRepository _agencyRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public NewsController(INewsRepository repository,
            IReporterRepository reporterRepository,
            ICountryRepository countryRepository,
            ICategoryRepository categoryRepository,
            INewsAgencyRepository agencyRepository,
            IMapper mapper)
        {
            _repository = repository;
            _reporterRepository = reporterRepository;
            _countryRepository = countryRepository;
            _categoryRepository = categoryRepository;
            _agencyRepository = agencyRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetNews")]
        [ProducesResponseType(200)]

        public async Task<ActionResult<ICollection<NewsDto>>> GetNews()
        {
            return Ok(await _repository.GetAllNews());
        }

        [HttpGet("{CategoryID:int}", Name = "GetCategoryNews")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<ICollection<NewsDto>>> GetCategoryNews(int CategoryID)
        {
            try
            {
                var News = await _repository.GetNewsByCategory(CategoryID);
                if (News == new News())
                {
                    return NotFound();
                }
                return Ok(News);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{Title}", Name = "GetNewsByTitle")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<ICollection<NewsDto>>> GetNewsByTitle(string Title)
        {
            try
            {
                var news = await _repository.GetNewsByTitle(Title);
                if (news.Count > 0)
                {
                    return Ok(news);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{CountryID:int}/{CategoryID:int}", Name = "GetNewsByCountryCategories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<ICollection<NewsDto>>> GetNewsByCountryCategories(int CountryID, int CategoryID)
        {
            try
            {
                var News = await _repository.GetNewsByCountryFilter(CountryID, CategoryID);
                if (News.Count > 0)
                {
                    return Ok(News);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("{CategoryID:int}/{CountryID:int}/{ReporterID:int}", Name = "AddNewNews")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> AddNewNews(int CategoryID, int CountryID,
                    int ReporterID, [FromBody] NewsDto NewNews)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Country = await _countryRepository.GetCountryByID(CountryID);

                    if (Country == new Country())
                    {
                        return NotFound();
                    }

                    var Category = new Category();

                    if (CategoryID != 0)
                    {
                        Category = await _categoryRepository.GetCategoryByID(CategoryID);
                    }
                    else
                    {
                        Category = await _categoryRepository.GetNewsCategory("Common");
                    }

                    var Reporter = await _reporterRepository.GetReporterByID(ReporterID);
                    if (Reporter == new Reporter())
                    {
                        return NotFound();
                    }

                    var NewsMap = _mapper.Map<News>(NewNews);


                    // Relations 

                    var Agency = await _agencyRepository.GetNewsAgencyByName(Reporter.NewsAgencyName);
                    NewsMap.Reporter = Reporter;
                    NewsMap.NewsAgency = Agency;
                    NewsMap.NewsAgencyName = Reporter.NewsAgencyName;
                    NewsMap.ReporterName = Reporter.Name;


                    NewsMap.CountryCategories = new CountryCategories()
                    {
                        CategoryID = CategoryID,
                        CountryID = CountryID,
                        Category = Category,
                        Country = Country
                    };

                    if (await _repository.AddNews(NewsMap, CountryID, CategoryID))
                    {
                        return Ok("Successfully");
                    }
                    return BadRequest();

                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{NewsID:int}/{ReporterID:int}/{CategoryID:int}/{CountryID:int}", Name = "UpdateNews")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> UpdateNews(int NewsID, int ReporterID, int CategoryID, int CountryID
                                , [FromBody] NewsDto NewNews)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var NewsMap = _mapper.Map<News>(NewNews);
                    var Reporter = await _reporterRepository.GetReporterByID(NewsID);

                    if (Reporter == new Reporter())
                    {
                        return NotFound();
                    }

                    NewsMap.Reporter = Reporter;
                    NewsMap.NewsAgency = Reporter.NewsAgency;
                    NewsMap.ReporterName = Reporter.Name;
                    NewsMap.NewsAgencyName = Reporter.NewsAgencyName;

                    var Country = await _countryRepository.GetCountryByID(CountryID);

                    if (Country == new Country())
                    {
                        return NotFound();
                    }

                    var Category = new Category();

                    if (CategoryID == 0)
                    {
                        Category = await _categoryRepository.GetCategoryByID(CategoryID);
                    }
                    else
                    {
                        Category = await _categoryRepository.GetNewsCategory("Common");
                    }

                    NewsMap.CountryCategories = new CountryCategories()
                    {
                        Category = Category,
                        CategoryID = CategoryID,
                        Country = Country,
                        CountryID = CountryID,
                    };

                    if (await _repository.UpdateNews(NewsMap))
                    {
                        return Ok("Successfully");
                    }
                    return NotFound();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{NewsID:int}", Name = "DeleteNews")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> DeleteNews(int NewsID)
        {
            try
            {
                var FindNews = await _repository.GetNewsByID(NewsID);

                if (FindNews == new News())
                {
                    return NotFound();
                }

                if (await _repository.DeleteNews(NewsID))
                {
                    return Ok("Successfully");
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
