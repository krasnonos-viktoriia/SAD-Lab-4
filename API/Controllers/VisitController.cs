using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
using API.DTOs;

namespace SAD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;
        private readonly IMapper _mapper;

        public VisitController(IVisitService visitService, IMapper mapper)
        {
            _visitService = visitService;
            _mapper = mapper;
        }

        // GET: api/visit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetAll()
        {
            var visits = await _visitService.GetAllAsync();
            return Ok(_mapper.Map<List<VisitDto>>(visits));
        }

        // GET: api/visit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitDto>> GetById(int id)
        {
            var visit = await _visitService.GetByIdAsync(id);
            if (visit == null)
                return NotFound();

            return Ok(_mapper.Map<VisitDto>(visit));
        }

        // GET: api/visit/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetByUserId(int userId)
        {
            var visits = await _visitService.GetByUserIdAsync(userId);
            return Ok(_mapper.Map<List<VisitDto>>(visits));
        }

        // POST: api/visit
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VisitDto dto)
        {
            var model = _mapper.Map<VisitModel>(dto);
            await _visitService.AddAsync(model);
            return Ok("Візит додано успішно");
        }

        //// PUT: api/visit/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] VisitDto dto)
        //{
        //    var model = _mapper.Map<VisitModel>(dto);
        //    model.Id = id;
        //    await _visitService.UpdateAsync(model);
        //    return Ok("Візит оновлено успішно");
        //}

        // DELETE: api/visit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _visitService.DeleteAsync(id);
            return Ok("Візит видалено успішно");
        }
    }
}
