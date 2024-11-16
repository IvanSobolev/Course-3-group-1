using LineAPI.Interface;

namespace LineAPI.Implimentation;

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class LineManager : ILineManager
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(4); // Ограничение до 4 потоков

    public LineManager(string filePath)
    {
        _filePath = filePath;
    }

    public async Task WriteLineAsync(string text)
    {
        await _semaphore.WaitAsync();
        try
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true, Encoding.UTF8))
            {
                await writer.WriteLineAsync(text);
            }
        }
        catch (IOException ex)
        {
            throw new IOException("Ошибка записи в файл.", ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<string> ReadAllAsync()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException("Файл не найден.");
        }

        using (StreamReader reader = new StreamReader(_filePath, Encoding.UTF8))
        {
            return await reader.ReadToEndAsync();
        }
    }
}