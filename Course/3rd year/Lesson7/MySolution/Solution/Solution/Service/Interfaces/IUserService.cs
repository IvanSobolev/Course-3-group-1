using Solution.Models;

namespace Solution.Service.Interfaces;

public interface IUserService
{
    public void AddUser(User? _newUser);
    public bool UpdateUser(int i, User _updateUser);
    public bool DeleteUser(int i);
}