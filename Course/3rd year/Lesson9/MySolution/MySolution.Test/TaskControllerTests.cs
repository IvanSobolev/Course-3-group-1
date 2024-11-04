using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySolution.Controllers;
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
        
        var controller = new TaskController(context);
        
        var beforeResult = await controller.GetTasks();

        var actionBeforeResult = Assert.IsType<ActionResult<List<MySolution.Models.Data.Task>>>(beforeResult);
        var returnBeforeResult = Assert.IsType<OkObjectResult>(actionBeforeResult.Result);
        var beforeTasks = Assert.IsAssignableFrom<List<MySolution.Models.Data.Task>>(returnBeforeResult.Value);
        var beforeCount = beforeTasks.Count;
        
        context.Tasks.Add(new Models.Data.Task { Name = "Test Task 1", Description = "Desc 1" });
        context.Tasks.Add(new Models.Data.Task { Name = "Test Task 2", Description = "Desc 2" });
        await context.SaveChangesAsync();

        var result = await controller.GetTasks();

        var actionResult = Assert.IsType<ActionResult<List<MySolution.Models.Data.Task>>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
        var tasks = Assert.IsAssignableFrom<List<MySolution.Models.Data.Task>>(returnValue.Value);
        Assert.Equal(beforeCount + 2, tasks.Count);
    }
}