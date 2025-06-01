using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
using API.DTOs;

namespace SAD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(_mapper.Map<List<UserDto>>(users));
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            var model = _mapper.Map<UserModel>(dto);
            await _userService.AddAsync(model);
            return Ok("Користувача додано успішно");
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto dto)
        {
            var model = _mapper.Map<UserModel>(dto);
            model.Id = id;
            await _userService.UpdateAsync(model);
            return Ok("Користувача оновлено успішно");
        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok("Користувача видалено успішно");
        }
    }
}
