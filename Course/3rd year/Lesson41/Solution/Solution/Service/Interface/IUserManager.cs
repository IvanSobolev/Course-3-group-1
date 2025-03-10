using Solution.Model.DTO;
using Solution.Model.Entity;
using Solution.Model.Structures;

namespace Solution.Service.Interface;

public interface IUserManager
{
    Task<OperationResult> AddNewUserAsync(NewUserDataDto userDataDto);
    Task<User?> GetUserAsync(string id);
    Task<IEnumerable<User>> GetUsersAsync(FilterViewModel filterViewModel);
    Task<OperationResult> UpdateUserAsync(UpdateUserDataDto userDataDto);
    Task<OperationResult> DeleteUserAsync(string id);
}