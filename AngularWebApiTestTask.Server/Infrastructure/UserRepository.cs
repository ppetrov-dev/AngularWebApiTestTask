using AngularWebApiTestTask.Server.Database;
using AngularWebApiTestTask.Server.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AngularWebApiTestTask.Server.Infrastructure;

internal class UserRepository(ApplicationDbContext context, IPasswordHasher<User> passwordHasher) : IUserRepository
{
    public async Task<User> AddUserAsync(User user)
    {
        user.Password = passwordHasher.HashPassword(user, user.Password);
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await context.Users.ToListAsync();
    }
}