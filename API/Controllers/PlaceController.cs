using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
using API.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public PlaceController(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        // GET: api/place
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceDto>>> GetAll()
        {
            var places = await _placeService.GetAllAsync();
            return Ok(_mapper.Map<List<PlaceDto>>(places));
        }

        // GET: api/place/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceDto>> GetById(int id)
        {
            var place = await _placeService.GetByIdAsync(id);
            if (place == null)
                return NotFound();

            return Ok(_mapper.Map<PlaceDto>(place));
        }

        // POST: api/place
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlaceDto dto)
        {
            var model = _mapper.Map<PlaceModel>(dto);
            await _placeService.AddAsync(model);
            return Ok("Місце додано успішно");
        }

        // PUT: api/place/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlaceDto dto)
        {
            var model = _mapper.Map<PlaceModel>(dto);
            model.Id = id;
            await _placeService.UpdateAsync(model);
            return Ok("Місце оновлено успішно");
        }

        // DELETE: api/place/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _placeService.DeleteAsync(id);
            return Ok("Місце видалено успішно");
        }
    }
}










//using Microsoft.AspNetCore.Mvc;
//using BLL.Interfaces;
//using AutoMapper;
//using BLL.Models;
//using API.DTOs;

//namespace SAD.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class PlaceController : ControllerBase
//    {
//        private readonly IPlaceService _placeService;
//        private readonly IMapper _mapper;

//        public PlaceController(IPlaceService placeService, IMapper mapper)
//        {
//            _placeService = placeService;
//            _mapper = mapper;
//        }

//        // GET: api/place
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<PlaceDto>>> GetAll()
//        {
//            var places = await _placeService.GetAllAsync();
//            var result = _mapper.Map<IEnumerable<PlaceDto>>(places);
//            return Ok(result);
//        }

//        // GET: api/place/{id}
//        [HttpGet("{id}")]
//        public async Task<ActionResult<PlaceDto>> GetById(int id)
//        {
//            var place = await _placeService.GetByIdAsync(id);
//            if (place == null)
//                return NotFound();

//            return Ok(_mapper.Map<PlaceDto>(place));
//        }

//        // POST: api/place
//        [HttpPost]
//        public async Task<ActionResult> Create([FromBody] PlaceDto dto)
//        {
//            var model = _mapper.Map<PlaceModel>(dto);
//            await _placeService.AddAsync(model);
//            return CreatedAtAction(nameof(GetById), new { id = model.Id }, dto);
//        }

//        // PUT: api/place/{id}
//        [HttpPut("{id}")]
//        public async Task<ActionResult> Update(int id, [FromBody] PlaceDto dto)
//        {
//            if (id != dto.Id)
//                return BadRequest("ID в URL не відповідає ID у тілі запиту");

//            var existing = await _placeService.GetByIdAsync(id);
//            if (existing == null)
//                return NotFound();

//            var model = _mapper.Map<PlaceModel>(dto);
//            await _placeService.UpdateAsync(model);
//            return NoContent();
//        }

//        // DELETE: api/place/{id}
//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delete(int id)
//        {
//            var existing = await _placeService.GetByIdAsync(id);
//            if (existing == null)
//                return NotFound();

//            await _placeService.DeleteAsync(id);
//            return NoContent();
//        }
//    }
//}
