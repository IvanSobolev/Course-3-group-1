using MySolution.Model;
using MySolution.Service.Interfaces;

namespace MySolution.Service.Implementation;

public class SqliteLoggerService(LogDataContext context) : ILoggerServices
{
    private LogDataContext _context = context;
    
    public void Log(string message)
    {
        Log log = new Log() { Id = _context.Logs.Count(), LogText = message };
        _context.Logs.Add(log);
        _context.SaveChanges();
    }
}