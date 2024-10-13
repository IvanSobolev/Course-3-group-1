namespace Solution.Models;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PaswordHash { get; set; }
    public List<Book> UserBasket { get; set; }
}