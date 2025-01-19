using AngularWebApiTestTask.Server.Database;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AngularWebApiTestTask.Server.Infrastructure;

internal class UserRepository(ApplicationDbContext context, IPasswordHasher<User> passwordHasher) : IUserRepository
{
    public async Task<UserDto> AddUserAsync(User user)
    {
        user.Password = passwordHasher.HashPassword(user, user.Password);
        context.Users.Add(user);
        await context.SaveChangesAsync();
        var userDto = await GetUserByIdAsync(user.Id);
        return userDto!;
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        return await GetAllUsersAsDtosQuery()
            .SingleOrDefaultAsync(user=> user.Id == id);
    }

    public Task<UserDto[]> GetAllUsersAsync()
    {
        var query = GetAllUsersAsDtosQuery();

        return query.ToArrayAsync();
    }

    private IQueryable<UserDto> GetAllUsersAsDtosQuery()
    {
        return from user in context.Users.Include(u=>u.Province)
            join country in context.Countries on user.Province.CountryId equals country.Id
            select new UserDto(user.Id, user.Login, user.AgreeToTerms, user.Province.Name, country.Name);
    }
}