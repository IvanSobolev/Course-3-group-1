using Solution.Model.Entity;
using Solution.Model.Structures;

namespace Solution.Repository.Interface;

public interface IUserRepository
{
    Task<OperationResult> AddNewUserAsync(string login, string paswordHash);
    Task<User?> GetUserAsync(string id);
    Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize, string filterLogin = "", 
                                        string filterPasswordHash = "", bool sortedByLogin = false);
    Task<OperationResult> UpdateUserAsync(string id, string? newLogin, string? newPasswordHash);
    Task<OperationResult> DeleteUserAsync(string id);
}