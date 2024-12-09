using MySolution.Service.Interfaces;

namespace MySolution.Service.Implementation;

public class ConsoleLoggerService : ILoggerServices
{
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}