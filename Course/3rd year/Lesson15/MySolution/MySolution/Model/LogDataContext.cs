using Microsoft.EntityFrameworkCore;

namespace MySolution.Model;

public class LogDataContext(DbContextOptions<LogDataContext> options) : DbContext(options)
{
    public DbSet<Log> Logs { get; set; }
}