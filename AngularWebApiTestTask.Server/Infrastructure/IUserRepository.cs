using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Infrastructure;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<User?> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
}