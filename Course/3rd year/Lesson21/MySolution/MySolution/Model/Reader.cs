namespace MySolution.Model;

public class Reader
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Book> ReadedBooks { get; set; }
}