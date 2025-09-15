namespace Core;

public interface IUserRepository
{
    Task<User?> GetUserById(int id);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> AddUser(User user);
    Task UpdateUser(User user);
    Task<bool> DeleteUser(int id);
}
