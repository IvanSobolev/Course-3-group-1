namespace MySolution.Model;

public struct Users
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public string PasswordHash { get; set; }
    public string PhoneNumber { get; set; }
}