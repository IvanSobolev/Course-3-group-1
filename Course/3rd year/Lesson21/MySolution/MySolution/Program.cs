using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySolution.Model;

namespace MySolution;

class Program
{
    static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<Context>(options =>
                options.UseSqlite("Data Source=library.db"))
            .BuildServiceProvider();

        using (var context = serviceProvider.GetService<Context>())
        {
            context.Database.Migrate();
        }
    }
}