namespace LineAPI.Interface;

public interface ILineManager
{
    public Task WriteLineAsync(string text);
    public Task<string> ReadAllAsync();
}