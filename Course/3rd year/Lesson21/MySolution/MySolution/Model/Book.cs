namespace MySolution.Model;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Author _Author { get; set; }
}