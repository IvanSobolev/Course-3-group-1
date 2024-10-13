using Solution.Models;
using Solution.Service.Interfaces;

namespace Solution.Service.Implementations;

public class BookService(List<Book> books) : IBookService
{
    public void AddBook(Book newBook)
    {
        books.Add(newBook ?? throw new ArgumentNullException(nameof(newBook), "Record cannot be null"));
    }

    public bool UpdateBook(int i, Book updateBook)
    {
        if (i < 0 || i >= books.Count)
        {
            return false;
        }
        
        books[i] = updateBook ?? throw new ArgumentNullException(nameof(updateBook), "Record cannot be null");
        return true;
    }

    public bool DeleteBook(int i)
    {
        if (i < 0 || i >= books.Count)
        {
            return false;
        }

        books.RemoveAt(i);
        return true;
    }

    public List<Book> GetAll()
    {
        return books;
    }
}