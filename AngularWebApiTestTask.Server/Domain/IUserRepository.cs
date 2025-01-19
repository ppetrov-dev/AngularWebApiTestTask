using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Domain;

public interface IUserRepository
{
    Task<UserDto> AddUserAsync(User user);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto[]> GetAllUsersAsync();
}