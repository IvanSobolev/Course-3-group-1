using System.Data;
using Microsoft.EntityFrameworkCore;
using MySolution.Manager.Interfaces;
using MySolution.Models;
using Task = MySolution.Models.Data.Task;

namespace MySolution.Manager.Implementations;

public class TaskManager(DataContext context) : ITaskManager
{
    private readonly DataContext _context = context;

    public async Task<List<Task>> GetTasksAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<Task?> GetTaskAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<Task> AddTaskAsync(Task newTask)
    {
        _context.Tasks.Add(newTask);
        await _context.SaveChangesAsync();
        return newTask;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        
        return true;
    }
}