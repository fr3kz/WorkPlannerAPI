using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WorkPlanner.Entities;
using Task = WorkPlanner.Entities.Task;

namespace WorkPlanner.Controllers;

[Route("api/tasks")]
[ApiController]
public class TasksController : Controller
{

    private  List<Task> tasks = new List<Task>(); 
    
    
    [HttpGet]
    public IActionResult GetAllTasks()
    {
        return Ok(tasks);
    }

    [HttpGet]
    public IActionResult GetTasksByStatus([FromQuery] int status)
    {
        switch (status)
        {
           case 0:  return Ok( tasks.Where(t => t.Status == Task.TaskStatus.Planned));
           case 1: return Ok(  tasks.Where(t => t.Status == Task.TaskStatus.WorkingAt));
           case 2:  return Ok(tasks.Where(t => t.Status == Task.TaskStatus.Done)); 
           default: return BadRequest();
        }
        
       
    }

    [HttpGet("{id}")]
    public IActionResult GetTask(Guid id)
    {
        var _task = tasks.FirstOrDefault(t => t.ID == id);
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
        tasks.Add(task);
        
        return CreatedAtAction(nameof(GetTask), new { id = task.ID }, task);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateTask(Guid id, [FromBody] Task updatedTask)
    {
        if (updatedTask == null || id != updatedTask.ID)
        {
            return BadRequest();
        }

        var existingTask = tasks.FirstOrDefault(t => t.ID == id);
        if (existingTask == null)
        {
            return NotFound();
        }

        
        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.Status = updatedTask.Status;
        existingTask.Duration = updatedTask.Duration;
        existingTask.Date = updatedTask.Date;

        return NoContent(); 
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(Guid id)
    {
        var task = tasks.FirstOrDefault(t => t.ID == id);
        if (task == null)
        {
            return NotFound();
        }

        tasks.Remove(task);
        return NoContent(); 
    }
}