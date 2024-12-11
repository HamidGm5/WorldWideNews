using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorldWideNews.API.Entities;
using WorldWideNews.API.Repository.Interfaces;
using WorldWideNews.Models.Dtos;

namespace WorldWideNews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class NewsAgencyController : Controller
    {
        private readonly INewsAgencyRepository _repository;
        private readonly IMapper _mapper;

        public NewsAgencyController(INewsAgencyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetNewsAgencies")]
        [ProducesResponseType(200)]

        public async Task<ActionResult<ICollection<NewsAgencyDto>>> GetNewsAgencies()
        {
            return Ok(await _repository.GetNewsAgencies());
        }

        [HttpGet("{NewsAgencyName}", Name = "GetNewsAgencyByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<NewsAgencyDto>> GetNewsAgencyByName(string NewsAgencyName)
        {
            try
            {
                var NewsAgency = await _repository.GetNewsAgencyByName(NewsAgencyName);
                if (NewsAgency != new NewsAgency())
                {
                    return Ok(NewsAgency);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost(Name = "AddNewsAgency")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> AddNewsAgency([FromBody] NewsAgencyDto newNewsAgency)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var NewsAgencyMap = _mapper.Map<NewsAgency>(newNewsAgency);
                    if (await _repository.AddNewsAgency(NewsAgencyMap))
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

        [HttpPut("{NewsAgencyID:int}", Name = "UpdateNewsAgency")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> UpdateNewsAgency(int NewsAgencyID, [FromBody] NewsAgencyDto newNewsAgency)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (await _repository.GetNewsAgencyByID(NewsAgencyID) != new NewsAgency())
                {
                    var NewsAgencyMap = _mapper.Map<NewsAgency>(newNewsAgency);
                    if (await _repository.UpdateNewsAgency(NewsAgencyMap))
                    {
                        return Ok("Successfully");
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

        [HttpDelete("{NewsAgencyID:int}", Name = "DeleteNewsAgency")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> DeleteNewsAgency(int NewsAgencyID)
        {
            try
            {
                if (await _repository.GetNewsAgencyByID(NewsAgencyID) != new NewsAgency())
                {
                    if (await _repository.DeleteNewsAgency(NewsAgencyID))
                    {
                        return Ok("Successfully");
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
