using Core;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModel> CreateUser(UserModel user)
    {
        User toReturn = await _userRepository.AddUser(user.ToEntity());
        return toReturn.ToModel();
    }

    public async Task<bool> DeleteUser(int id)
    {
        return await _userRepository.DeleteUser(id);
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        var allUsers = await _userRepository.GetAllUsers();
        return allUsers.ToModels();
    }

    public async Task<UserModel?> GetUserById(int id)
    {
        User? user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            return null;
        }
        return user.ToModel();
    }

    public async Task UpdateUser(UserModel userModel)
    {
        await _userRepository.UpdateUser(userModel.ToEntity());
    }
}

