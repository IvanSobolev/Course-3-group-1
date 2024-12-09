using MySolution.Model;
using MySolution.Service.Interfaces;

namespace MySolution.Service.Implementation;

public class SqliteNoteRepository(DataContext dataContext) : INoteRepository
{
    private DataContext _context = dataContext;
    
    public IEnumerable<Note> GetAllNotes()
    {
        return _context.Notes.ToList();
    }

    public Note? GetNoteById(int id)
    {
        return _context.Notes.FirstOrDefault(n => n.Id == id);
    }

    public void AddNote(Note note)
    {
        _context.Notes.Add(note);
        _context.SaveChanges();
    }

    public void UpdateNote(Note note)
    {
        var existing = _context.Notes.FirstOrDefault(c => c.Id == note.Id);

        if (existing != null)
        {
            _context.Notes.Update(note);
            _context.SaveChanges();
        }
    }

    public void DeleteNoteById(int id)
    {
        var existing = _context.Notes.FirstOrDefault(c => c.Id == id);

        if (existing != null)
        {
            _context.Notes.Remove(existing);
            _context.SaveChanges();
        }
    }
}