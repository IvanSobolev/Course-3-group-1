using LineAPI.Implimentation;
using LineAPI.Interface;

namespace LineAPI.Controllers;


using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class LineController : ControllerBase
{
    private readonly ILineManager _fileProcessor = new LineManager("output.txt");
    

    [HttpPost("process")]
    public async Task<IActionResult> ProcessFile([FromBody] string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("Текст не может быть пустым.");
        }

        try
        {
            await _fileProcessor.WriteLineAsync(text);
            var content = await _fileProcessor.ReadAllAsync();
            return Ok(content); // Возвращаем содержимое файла
        }
        catch (FileNotFoundException)
        {
            return NotFound("Файл не найден.");
        }
        catch (IOException ex)
        {
            return StatusCode(500, $"Ошибка при работе с файлом: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }
}