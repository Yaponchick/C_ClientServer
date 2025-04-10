using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public GradesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grade>>> GetAllGrades()
        {
            var grades = await _context.Grades.AsNoTracking().ToListAsync();
            return Ok(grades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Grade>> GetGradeById(int id)
        {
            var grade = await _context.Grades.FirstOrDefaultAsync(g => g.GradeID == id);

            if (grade == null)
            {
                return NotFound($"Оценка с ID {id} не найдена.");
            }

            return Ok(grade);
        }

        [HttpPost]
        public async Task<ActionResult<Grade>> AddNewGrade([FromBody] Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGradeById), new { id = grade.GradeID }, grade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyGrade(int id, [FromBody] Grade updatedGrade)
        {
            if (id != updatedGrade.GradeID)
            {
                return BadRequest("ID оценки в запросе не соответствует ID в данных.");
            }

            var existingGrade = await _context.Grades.FindAsync(id);
            if (existingGrade == null)
            {
                return NotFound($"Оценка с ID {id} не найдена.");
            }

            _context.Entry(existingGrade).CurrentValues.SetValues(updatedGrade);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Grades.AnyAsync(g => g.GradeID == id))
                {
                    return NotFound($"Оценка с ID {id} была удалена.");
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound($"Оценка с ID {id} не найдена.");
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}