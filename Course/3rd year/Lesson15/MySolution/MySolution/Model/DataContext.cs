using Microsoft.EntityFrameworkCore;

namespace MySolution.Model;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Note> Notes { get; set; }
}