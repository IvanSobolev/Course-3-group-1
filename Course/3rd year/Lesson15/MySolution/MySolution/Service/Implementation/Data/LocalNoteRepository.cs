using MySolution.Model;
using MySolution.Service.Interfaces;

namespace MySolution.Service.Implementation;

public class LocalNoteRepository : INoteRepository
{
    private readonly List<Note> _notes = new List<Note>();
    
    public IEnumerable<Note> GetAllNotes()
    {
        return _notes;
    }

    public Note? GetNoteById(int id)
    {
        return _notes.FirstOrDefault(n => n.Id == id);
    }

    public void AddNote(Note note)
    {
        _notes.Add(note);
    }

    public void UpdateNote(Note note)
    {
        var existing = GetNoteById(note.Id);
        if (existing != null)
        {
            existing.Title = note.Title;
            existing.Context = note.Context;
        }
    }

    public void DeleteNoteById(int id)
    {
        var existing = GetNoteById(id);
        if (existing != null)
        {
            _notes.Remove(existing);
        }
    }
}