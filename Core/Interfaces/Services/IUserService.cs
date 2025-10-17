namespace Core;


public interface IUserService
{
    Task<UserModel?> GetUserById(int id);
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<UserModel> CreateUser(UserModel user);
    Task UpdateUser(UserModel userModel);
    Task<bool> DeleteUser(int id);
}

