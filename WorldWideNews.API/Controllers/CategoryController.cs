using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorldWideNews.API.Entities;
using WorldWideNews.API.Repository.Interfaces;
using WorldWideNews.Models.Dtos;

namespace WorldWideNews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetCategories()
        {
            return Ok(await _repository.GetCategories());
        }

        [HttpGet("{CategoryName}", Name = "GetNewsCategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<CategoryDto>> GetNewsCategory(string CategoryName)
        {
            try
            {
                var Category = await _repository.GetNewsCategory(CategoryName);
                if (Category == null)
                {
                    return NotFound();
                }
                return Ok(Category);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost(Name = "AddNewsCategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> AddNewsCategory([FromBody] CategoryDto NewCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var CategoryMap = _mapper.Map<Category>(NewCategory);

                    if (await _repository.AddNewCategory(CategoryMap))
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

        [HttpPut("{CategoryID:int}", Name = "UpdateCategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> UpdateCategory(int CategoryID, [FromBody] CategoryDto UpdateCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (await _repository.GetCategoryByID(CategoryID) != null)
                {
                    var CategoryMap = _mapper.Map<Category>(UpdateCategory);

                    if (await _repository.UpdateCategory(CategoryID, CategoryMap))
                    {
                        return Ok("Successfully");
                    }
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{CategoryID:int}", Name = "")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> DeleteCategory(int CategoryID)
        {
            try
            {
                if (await _repository.DeleteCategory(CategoryID))
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
