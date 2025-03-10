using Solution.Model.DTO;
using Solution.Model.Entity;
using Solution.Model.Structures;
using Solution.Repository.Interface;
using Solution.Service.Interface;

namespace Solution.Service.Implementation;

public class UserManager(IUserRepository userRepository) : IUserManager
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<OperationResult> AddNewUserAsync(NewUserDataDto userDataDto)
    {
        return await _userRepository.AddNewUserAsync(userDataDto.Login, userDataDto.PasswordHash);
    }

    public async Task<User?> GetUserAsync(string id)
    {
        return await _userRepository.GetUserAsync(id);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(FilterViewModel filterViewModel)
    {
        return await _userRepository.GetUsersAsync(filterViewModel.Page, filterViewModel.PageSize, filterViewModel.FilterLogin, filterViewModel.FilterPasswordHash, filterViewModel.SortedByLogin);
    }

    public async Task<OperationResult> UpdateUserAsync(UpdateUserDataDto userDataDto)
    {
        return await _userRepository.UpdateUserAsync(userDataDto.Id, userDataDto.Login, userDataDto.PasswordHash);
    }

    public async Task<OperationResult> DeleteUserAsync(string id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }
}