using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
using API.DTOs;

namespace SAD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        // GET: api/question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAll()
        {
            var questions = await _questionService.GetAllAsync();
            return Ok(_mapper.Map<List<QuestionDto>>(questions));
        }

        // GET: api/question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetById(int id)
        {
            var question = await _questionService.GetByIdAsync(id);
            if (question == null)
                return NotFound();

            return Ok(_mapper.Map<QuestionDto>(question));
        }

        // POST: api/question
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionDto dto)
        {
            var model = _mapper.Map<QuestionModel>(dto);
            await _questionService.AddAsync(model);
            return Ok("Питання додано успішно");
        }

        // PUT: api/question/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] QuestionDto dto)
        {
            var model = _mapper.Map<QuestionModel>(dto);
            model.Id = id;
            await _questionService.UpdateAsync(model);
            return Ok("Питання оновлено успішно");
        }

        // DELETE: api/question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionService.DeleteAsync(id);
            return Ok("Питання видалено успішно");
        }
    }
}
