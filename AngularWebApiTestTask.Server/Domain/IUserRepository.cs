using AngularWebApiTestTask.Server.Contracts;
using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Domain;

public interface IUserRepository
{
    Task<CreateUserResponse> AddUserAsync(User user, CancellationToken cancellationToken);
    Task<UserResponse?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    Task<UserResponse[]> GetAllUsersAsync(CancellationToken cancellationToken);
}