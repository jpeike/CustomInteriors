namespace Core;

public static class UserExtensions
{
    public static UserModel ToModel(this User user) => new UserModel
    {
        Id = user.Id,
        CustomerId = user.CustomerId,
        Username = user.Username,
        Email = user.Email,
        CreatedOn = user.CreatedOn
    };

    public static IEnumerable<UserModel> ToModels(this IEnumerable<User> users)
    {
        return users.Select(u => u.ToModel());
    }

    public static User ToEntity(this UserModel model) => new User
    {
        Id = model.Id,
        CustomerId = model.CustomerId,
        Username = model.Username,
        Email = model.Email,
        CreatedOn = model.CreatedOn,
        // PasswordHash would need to be set separately
    };
}

