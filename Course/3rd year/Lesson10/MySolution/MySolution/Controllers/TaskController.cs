using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySolution.Manager.Interfaces;
using MySolution.Models;
using Task = MySolution.Models.Data.Task;

namespace MySolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController (ITaskManager taskManager) : ControllerBase
    {
        private readonly ITaskManager _taskManager = taskManager;

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await _taskManager.GetTasksAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskWithId(int id)
        {
            return Ok(await _taskManager.GetTasksAsync());
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTask([FromBody] Task newTask)
        {
            return Ok(await _taskManager.AddTaskAsync(newTask));
        }
    
        [HttpDelete("del{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (await _taskManager.DeleteTaskAsync(id))
            { return Ok(true); }
            else
            { return NotFound(false); }
        }
    }
}
