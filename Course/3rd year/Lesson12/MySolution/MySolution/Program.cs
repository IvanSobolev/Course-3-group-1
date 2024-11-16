namespace MySolution;

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class FileProcessor
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(4); // Ограничение до 4 потоков

    public FileProcessor(string filePath)
    {
        _filePath = filePath;
    }

    public async Task WriteLineAsync(string text)
    {
        await _semaphore.WaitAsync();
        try
        {
            // Асинхронная запись строки в файл
            using (StreamWriter writer = new StreamWriter(_filePath, true, Encoding.UTF8))
            {
                await writer.WriteLineAsync(text);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка записи в файл: {ex.Message}");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<string> ReadAllAsync()
    {
        using (StreamReader reader = new StreamReader(_filePath, Encoding.UTF8))
        {
            return await reader.ReadToEndAsync();
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        string filePath = "output.txt";
        var fileProcessor = new FileProcessor(filePath);

        // Удаляем старый файл, если он существует
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        Console.WriteLine("Начинаем многопоточную запись в файл...");
        var tasks = new Task[10]; // Создаем 10 задач для записи в файл

        for (int i = 0; i < tasks.Length; i++)
        {
            string text = $"Строка из задачи {i + 1}";
            tasks[i] = fileProcessor.WriteLineAsync(text);
        }

        await Task.WhenAll(tasks); // Ожидаем завершения всех задач

        Console.WriteLine("\nСодержимое файла после многопоточной записи:");
        string content = await fileProcessor.ReadAllAsync();
        Console.WriteLine(content);
    }
}
