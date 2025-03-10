namespace Solution.Model.Structures;

public class UpdateUserDataDto
{
    public string Id { get; set; }
    public string? Login { get; set; }
    public string? PasswordHash { get; set; }
}