namespace MySolution.Models.Data;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DeadLine { get; set; }
    public DateTime CreateTime { get; set; }
    public bool IsComplited { get; set; }
}