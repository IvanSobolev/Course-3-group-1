using Microsoft.EntityFrameworkCore;

namespace MySolution.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<Data.Task> Tasks { get; set; }
}