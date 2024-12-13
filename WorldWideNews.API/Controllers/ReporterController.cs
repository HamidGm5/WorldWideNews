using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorldWideNews.API.Repository.Interfaces;
using WorldWideNews.API.Entities;
using WorldWideNews.Models.Dtos;

namespace WorldWideNews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporterController : Controller
    {
        private readonly IReporterRepository _repository;
        private readonly IMapper _mapper;
        private readonly INewsAgencyRepository _newsAgencyRepository;

        public ReporterController(IReporterRepository repository, IMapper mapper,
                INewsAgencyRepository newsAgencyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _newsAgencyRepository = newsAgencyRepository;
        }

        [HttpGet(Name = "GetReporters")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ICollection<ReporterDto>>> GetReporters()
        {
            return Ok(await _repository.GetReporters());
        }

        [HttpGet("{AgencyName}", Name = "GetAgencyReporters")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<ICollection<ReporterDto>>> GetAgencyReporters(string AgencyName)
        {
            try
            {
                var Reporters = await _repository.GetAgencyReporters(AgencyName);
                if (Reporters.Count > 0)
                {
                    return Ok(Reporters);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{ReporterID:int}", Name = "GetReporterByID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<ReporterDto>> GetReporterByID(int ReporterID)
        {
            try
            {
                var reporter = await _repository.GetReporterByID(ReporterID);
                if (reporter == null )
                {
                    return NotFound();
                }
                return Ok(reporter);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("{AgencyID:int}", Name = "AddNewReporter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> AddNewReporter(int AgencyID, [FromBody] ReporterDto newReporter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ReporterMap = _mapper.Map<Reporter>(newReporter);
                    var NewsAgency = await _newsAgencyRepository.GetNewsAgencyByID(AgencyID);
                    ReporterMap.NewsAgency = NewsAgency;
                    ReporterMap.NewsAgencyName = NewsAgency.Name;

                    if (await _repository.AddNewReporter(ReporterMap))
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

        [HttpPut("{ReporterID:int}", Name = "UpdateReporter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> UpdateReporter(int ReporterID, [FromBody] ReporterDto UpdateReporter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _repository.GetReporterByID(ReporterID) != null)
                    {
                        var ReporterMap = _mapper.Map<Reporter>(UpdateReporter);

                        if (await _repository.UpdateReporter(ReporterID,ReporterMap))
                        {
                            return Ok("Successfully");
                        }
                        return BadRequest();
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

        [HttpDelete("{ReporterID:int}", Name = "DeleteReporter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> DeleteReporter(int ReporterID)
        {
            try
            {
                var FindReporter = await _repository.GetReporterByID(ReporterID);
                if (FindReporter != new Reporter())
                {
                    if (await _repository.DeleteReporter(ReporterID))
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
