using AngularWebApiTestTask.Server.Contracts;
using AngularWebApiTestTask.Server.Database;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AngularWebApiTestTask.Server.Infrastructure;

internal class UserRepository(ApplicationDbContext context, IPasswordHasher<User> passwordHasher) : IUserRepository
{
    public async Task<CreateUserResponse> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            user.Password = passwordHasher.HashPassword(user, user.Password);

            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateUserResponse(user.Login, null);
        }
        catch (Exception e)
        {
            return new CreateUserResponse(null, e.Message);
        }
    }

    public async Task<UserResponse?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Include(u => u.Province)
            .ThenInclude(p => p!.Country)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        return user is null 
            ? null 
            : new UserResponse(user!.Id, user.Login, user.Province!.Name, user.Province.Country!.Name);
    }

    public Task<UserResponse[]> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var query = from user in context.Users
                    join province in context.Provinces on user.ProvinceId equals province.Id
                    join country in context.Countries on province.CountryId equals country.Id
                    select new UserResponse(user.Id, user.Login, province.Name, country.Name);

        return query.ToArrayAsync(cancellationToken);
    }
}