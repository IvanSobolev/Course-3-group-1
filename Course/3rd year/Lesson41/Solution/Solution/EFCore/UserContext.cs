using Microsoft.EntityFrameworkCore;
using Solution.Model.Entity;

namespace Solution.EFCore;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}