using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
using API.DTOs;

namespace SAD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaFileController : ControllerBase
    {
        private readonly IMediaFileService _mediaFileService;
        private readonly IMapper _mapper;

        public MediaFileController(IMediaFileService mediaFileService, IMapper mapper)
        {
            _mediaFileService = mediaFileService;
            _mapper = mapper;
        }

        // GET: api/mediafile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaFileDto>>> GetAll()
        {
            var files = await _mediaFileService.GetAllAsync();
            return Ok(_mapper.Map<List<MediaFileDto>>(files));
        }

        // GET: api/mediafile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaFileDto>> GetById(int id)
        {
            var file = await _mediaFileService.GetByIdAsync(id);
            if (file == null)
                return NotFound();

            return Ok(_mapper.Map<MediaFileDto>(file));
        }

        // POST: api/mediafile
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MediaFileDto dto)
        {
            var model = _mapper.Map<MediaFileModel>(dto);
            await _mediaFileService.AddAsync(model);
            return Ok("Медіафайл додано успішно");
        }

        //// PUT: api/mediafile/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] MediaFileDto dto)
        //{
        //    var model = _mapper.Map<MediaFileModel>(dto);
        //    model.Id = id;
        //    await _mediaFileService.UpdateAsync(model);
        //    return Ok("Медіафайл оновлено успішно");
        //}

        // DELETE: api/mediafile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediaFileService.DeleteAsync(id);
            return Ok("Медіафайл видалено успішно");
        }

        // GET: api/mediafile/place/5
        [HttpGet("place/{placeId}")]
        public async Task<ActionResult<IEnumerable<MediaFileDto>>> GetByPlaceId(int placeId)
        {
            var files = await _mediaFileService.GetByPlaceIdAsync(placeId);
            return Ok(_mapper.Map<List<MediaFileDto>>(files));
        }

    }
}
