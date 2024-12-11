using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorldWideNews.API.Entities;
using WorldWideNews.API.Repository.Interfaces;
using WorldWideNews.Models.Dtos;

namespace WorldWideNews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _repository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCountries")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ICollection<CountryDto>>> GetCountries()
        {
            return Ok(await _repository.GetCountries());
        }

        [HttpGet("{ID:int}" , Name = "GetCountryByID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<CountryDto>> GetCountryByID(int ID)
        {
            try
            {
                var Country = await _repository.GetCountryByID(ID);
                if (Country == null)
                {
                    return NotFound();
                }
                return Ok(Country);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{CountriesName}", Name = "GetCountriesByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<ICollection<CountryDto>>> GetCountriesByName(string CountriesName)
        {
            try
            {
                var Countries = await _repository.GetCountriesByName(CountriesName);
                if (Countries.Count > 0)
                {
                    return Ok(Countries);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost(Name = "AddNewCountry")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> AddNewCountry([FromBody] CountryDto NewCountry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var CountryMap = _mapper.Map<Country>(NewCountry);
                    if (await _repository.AddNewCountry(CountryMap))
                    {
                        return Ok("SuccessFully");
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

        [HttpPut(Name = "UpdateCountry")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> UpdateCountry([FromBody] CountryDto UpdateCountry)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (await _repository.GetCountryByID(UpdateCountry.ID) != null)
                {
                    var CountryMap = _mapper.Map<Country>(UpdateCountry);
                    if (await _repository.UpdateCountry(CountryMap))
                    {
                        return Ok("SuccessFully");
                    }
                    return BadRequest();
                }

                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{CountryName}" , Name = "DeleteCountry")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> DeleteCountry(string CountryName)
        {
            try
            {
                var DeleteCountry = await _repository.GetCountriesByName(CountryName);
                if (DeleteCountry.Count == 1)
                {
                    if (await _repository.DeleteCountry(CountryName))
                    {
                        return Ok("SuccessFully");
                    }
                    return BadRequest();
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
