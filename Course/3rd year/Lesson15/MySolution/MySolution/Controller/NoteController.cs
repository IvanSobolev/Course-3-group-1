using Microsoft.AspNetCore.Mvc;
using MySolution.Model;
using MySolution.Service.Interfaces;

namespace MySolution.Controller;

[ApiController]
[Route("api/[controller]")]
public class NoteController(INoteRepository noteRepository, ILoggerServices loggerService) : ControllerBase
{
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly ILoggerServices _logger = loggerService;

    [HttpGet]
    public IActionResult GetAllNotes()
    {
        var notes = _noteRepository.GetAllNotes();
        _logger.Log("Retrieved all notes");
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public IActionResult GetNoteById(int id)
    {
        var note = _noteRepository.GetNoteById(id);
        if (note == null)
        {
            _logger.Log($"Note with Id {{id}} not found");
            return NotFound();
        }
        _logger.Log($"Retrieved note with Id {id}");
        return Ok(note);
    }

    [HttpPost]
    public IActionResult CreateNote([FromBody] Note note)
    {
        _noteRepository.AddNote(note);
        _logger.Log($"Created note with id {note.Id}");
        return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateNote(int id, [FromBody] Note updatedNote)
    {
        if (id != updatedNote.Id)
        {
            return BadRequest();
        }

        var exist = _noteRepository.GetNoteById(id);
        if (exist == null)
        {
            _logger.Log($"Note with Id {{id}} not found");
            return NotFound();
        }
        _noteRepository.UpdateNote(updatedNote);
        _logger.Log($"Updated note with ID {id}");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteNoteById(int id)
    {
        var exist = _noteRepository.GetNoteById(id);
        if (exist == null)
        {
            _logger.Log($"Note with Id {{id}} not found");
            return NotFound();
        }
        _noteRepository.DeleteNoteById(id);
        _logger.Log($"Deleted note with Id {id}");
        return NoContent();
    }
}