using Task = MySolution.Models.Data.Task;

namespace MySolution.Manager.Interfaces;

public interface ITaskManager
{
    public Task<List<Task>> GetTasksAsync();
    public Task<Task?> GetTaskAsync(int id);
    public Task<Task> AddTaskAsync(Task newTask);
    public Task<bool> DeleteTaskAsync(int id);
}