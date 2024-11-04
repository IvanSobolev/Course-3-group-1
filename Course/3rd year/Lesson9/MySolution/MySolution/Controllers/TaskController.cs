using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySolution.Models;
using Task = MySolution.Models.Data.Task;

namespace MySolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController (DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<MySolution.Models.Data.Task>>> GetTasks()
        {
            return Ok(await _context.Tasks.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<MySolution.Models.Data.Task>>> GetTask(int id)
        {
            return Ok(await _context.Tasks.FindAsync(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTask([FromBody] Task newTask)
        {
            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();
            return Ok(newTask.Id);
        }
    
        [HttpPost("del{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        
            return Ok();
        }
    }
}
