using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySolution.Controllers;
using MySolution.Manager.Implementations;
using MySolution.Models;
using Task = System.Threading.Tasks.Task;


namespace MySolution.Test;

public class TaskControllerTests
{
    private DataContext GetDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite("Data source=Post.db");

        var taskContext = new DataContext(optionsBuilder.Options);
        taskContext.Database.EnsureCreated();

        return taskContext;
    }

    [Fact]
    public async Task GetTask_ReturnsTasks()
    {
        await using var context = GetDbContext();
        var tm = new TaskManager(context);
        
        var controller = new TaskController(tm);
        
        var beforeResult = await controller.GetTasks();

        var actionBeforeResult = Assert.IsAssignableFrom<IActionResult>(beforeResult);
        var returnBeforeResult = Assert.IsType<OkObjectResult>(actionBeforeResult);
        var beforeTasks = Assert.IsAssignableFrom<List<MySolution.Models.Data.Task>>(returnBeforeResult.Value);
        var beforeCount = beforeTasks.Count;
        
        context.Tasks.Add(new Models.Data.Task { Name = "Test Task 1", Description = "Desc 1" });
        context.Tasks.Add(new Models.Data.Task { Name = "Test Task 2", Description = "Desc 2" });
        await context.SaveChangesAsync();

        var result = await controller.GetTasks();

        var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
        var returnValue = Assert.IsType<OkObjectResult>(actionResult);
        var tasks = Assert.IsAssignableFrom<List<MySolution.Models.Data.Task>>(returnValue.Value);
        Assert.Equal(beforeCount + 2, tasks.Count);
    }
    [Fact]
    public async Task AddTask_ReturnsTasks()
    {
        await using var context = GetDbContext();
        var tm = new TaskManager(context);
        
        var controller = new TaskController(tm);
        
        var beforeResult = await controller.GetTasks();

        var actionBeforeResult = Assert.IsAssignableFrom<IActionResult>(beforeResult);
        var returnBeforeResult = Assert.IsType<OkObjectResult>(actionBeforeResult);
        var beforeTasks = Assert.IsAssignableFrom<List<MySolution.Models.Data.Task>>(returnBeforeResult.Value);
        var beforeCount = beforeTasks.Count;
        
        var t1 = new Models.Data.Task { Name = "Test Task 1", Description = "Desc 1" };
        var t2 = new Models.Data.Task { Name = "Test Task 2", Description = "Desc 2" };

        await controller.AddTask(t1);
        await controller.AddTask(t2);

        var result = await controller.GetTasks();

        var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
        var returnValue = Assert.IsType<OkObjectResult>(actionResult);
        var tasks = Assert.IsAssignableFrom<List<MySolution.Models.Data.Task>>(returnValue.Value);
        Assert.Equal(beforeCount + 2, tasks.Count);
    }
    
    [Fact]
    public async Task DeleteTask_ReturnsTasks()
    {
        await using var context = GetDbContext();
        var tm = new TaskManager(context);
        
        var controller = new TaskController(tm);
        
        var beforeResult = await controller.GetTasks();

        var actionBeforeResult = Assert.IsAssignableFrom<IActionResult>(beforeResult);
        var returnBeforeResult = Assert.IsType<OkObjectResult>(actionBeforeResult);
        var beforeTasks = Assert.IsAssignableFrom<List<MySolution.Models.Data.Task>>(returnBeforeResult.Value);
        var beforeCount = beforeTasks.Count;

        var result = await controller.DeleteTask(beforeTasks[^1].Id);

        if (beforeTasks[^1].Id <= 1)
        {
            Assert.Equal(0, beforeCount - 1);
        }
        else
        {
            var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
            var returnValue = Assert.IsType<OkObjectResult>(actionResult);
            var resp = Assert.IsAssignableFrom<bool>(returnValue.Value);
            Assert.True(resp);
        }
    }
}