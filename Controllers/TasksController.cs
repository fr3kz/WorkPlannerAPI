using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WorkPlanner.Data;
using WorkPlanner.Entities;

using Task = WorkPlanner.Entities.Task;

namespace WorkPlanner.Controllers;

[Route("api/tasks")]
[ApiController]
public class TasksController : Controller
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }
    
    
    
    [HttpGet]
    public IActionResult GetAllTasks()
    {
        return Ok(_context.Tasks.ToList());
    }

    [HttpGet("status")]
    public IActionResult GetTasksByStatus([FromQuery] Task.TaskStatus status)
    {
        var tasks = _context.Tasks.Where(t => t.Status == status).ToList();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetTask(Guid id)
    {
        var _task = _context.Tasks.FirstOrDefault(t => t.ID == id);
        if (_task == null)
        {
            return NotFound("Task not found");
        }
        return Ok(_task);
    }

    [HttpPost]
    public IActionResult AddTask([FromBody] Task task)
    {
        if (task == null)
        {
            return BadRequest("Task is null");
        }
        
        task.ID = Guid.NewGuid();
        _context.Tasks.Add(task);
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetTask), new { id = task.ID }, task);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateTask(Guid id, [FromBody] Task updatedTask)
    {
        if (updatedTask == null || id != updatedTask.ID)
        {
            return BadRequest();
        }

        var existingTask = _context.Tasks.FirstOrDefault(t => t.ID == id);
        if (existingTask == null)
        {
            return NotFound();
        }

        
        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.Status = updatedTask.Status;
        existingTask.Duration = updatedTask.Duration;
        existingTask.Date = updatedTask.Date;
        _context.SaveChanges();
        return NoContent(); 
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(Guid id)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.ID == id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();
        return NoContent(); 
    }

    [HttpPatch("{id}/status")]
    public IActionResult SetTaskStatus(Guid id, [FromQuery] Task.TaskStatus status)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.ID == id);
            
        if (task == null) return NotFound();

        task.Status = status;
        _context.SaveChanges();
            
        return Ok("Task successfully updated");
    }
    
}