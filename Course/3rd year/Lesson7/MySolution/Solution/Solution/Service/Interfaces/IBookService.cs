using Solution.Models;

namespace Solution.Service.Interfaces;

public interface IBookService
{
    public void AddBook(Book newBook);
    public bool UpdateBook(int i, Book updateBook);
    public bool DeleteBook(int i);
}