using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
using API.DTOs;

namespace SAD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        // GET: api/review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
        {
            var reviews = await _reviewService.GetAllAsync();
            return Ok(_mapper.Map<List<ReviewDto>>(reviews));
        }

        // GET: api/review/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetById(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(_mapper.Map<ReviewDto>(review));
        }

        // POST: api/review
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewDto dto)
        {
            var model = _mapper.Map<ReviewModel>(dto);
            await _reviewService.AddAsync(model);
            return Ok("Відгук додано успішно");
        }

        // PUT: api/review/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReviewDto dto)
        {
            var model = _mapper.Map<ReviewModel>(dto);
            model.Id = id;
            await _reviewService.UpdateAsync(model);
            return Ok("Відгук оновлено успішно");
        }

        // DELETE: api/review/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reviewService.DeleteAsync(id);
            return Ok("Відгук видалено успішно");
        }

        // GET: api/review/place/5
        [HttpGet("place/{placeId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetByPlaceId(int placeId)
        {
            var reviews = await _reviewService.GetByPlaceIdAsync(placeId);
            return Ok(_mapper.Map<List<ReviewDto>>(reviews));
        }

    }
}
