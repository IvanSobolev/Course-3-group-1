using Solution.Models;
using Solution.Service.Interfaces;

namespace Solution.Service.Implementations;

public class UserService(List<User> users) : IUserService
{
    
    public void AddUser(User newUser)
    {
        users.Add(newUser ?? throw new ArgumentNullException(nameof(newUser), "Record cannot be null"));
    }

    public bool UpdateUser(int i, User updateUser)
    {
        if (i < 0 || i >= users.Count)
        {
            return false;
        }
        
        users[i] = updateUser ?? throw new ArgumentNullException(nameof(updateUser), "Record cannot be null");
        return true;
    }

    public bool DeleteUser(int i)
    {
        if (i < 0 || i >= users.Count)
        {
            return false;
        }

        users.RemoveAt(i);
        return true;
    }
    
    public List<User> GetAll()
    {
        return users;
    }
}