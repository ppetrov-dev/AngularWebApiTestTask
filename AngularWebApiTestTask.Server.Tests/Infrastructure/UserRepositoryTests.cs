using AngularWebApiTestTask.Server.Contracts;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using AngularWebApiTestTask.Server.Infrastructure;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AngularWebApiTestTask.Server.Tests.Infrastructure;

public class UserRepositoryTests : RepositoryBaseTests
{
    private readonly UserRepository _repository;
    private Mock<IPasswordHasher<User>> _passwordHasherMock;
    private const int DefaultUserIdInDbContext = 100500;
    private const int DefaultProvinceIdInDbContext = 203040;
    public UserRepositoryTests()
    {
        _passwordHasherMock = new Mock<IPasswordHasher<User>>();
        _passwordHasherMock.Setup(hasher => hasher.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
            .Returns<User, string>((_, password) => password);

        _repository = new UserRepository(Context, _passwordHasherMock.Object);
    }

    private async Task FillDbContext()
    {
        await Context.Countries.AddRangeAsync([
            new CountryBuilder { Id = 1, Name = "Country 1"}.Build(),
            new CountryBuilder { Id = 2, Name = "Country 2"}.Build(),
        ]);
        await Context.Provinces.AddRangeAsync([
            new ProvinceBuilder { Id = DefaultProvinceIdInDbContext, Name = "Province 1", CountryId = 1}.Build(),
            new ProvinceBuilder { Id = 2, Name = "Province 2", CountryId = 1 }.Build(),
            new ProvinceBuilder { Id = 3, Name = "Province 3", CountryId = 2 }.Build()
        ]);
        await Context.Users.AddRangeAsync([
            new UserBuilder{ Id = 1, Login = "Login 1", ProvinceId = DefaultProvinceIdInDbContext}.Build(),
            new UserBuilder{ Id = DefaultUserIdInDbContext, Login = "Login 2", ProvinceId = 2}.Build(),
            new UserBuilder{ Id = 3, Login = "Login 3", ProvinceId = 3}.Build(),
        ]);
        await Context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddsUser_WhenUserIsValid()
    {
        await FillDbContext();

        var actualUser = await _repository.AddUserAsync(new UserBuilder { ProvinceId = DefaultProvinceIdInDbContext }.Build(), CancellationTokenSource.Token);

        actualUser.Should().NotBeNull();
    }

    [Fact]
    public async Task AddsUser_WithHashedPassword()
    {
        const string inputtedPassword = "inputted_password";
        var user = new UserBuilder { Password = inputtedPassword }.Build();
        const string hashedPassword = "hashed_password";
        _passwordHasherMock.Setup(hasher => hasher.HashPassword(user, inputtedPassword))
            .Returns<User, string>((_, _) => hashedPassword);

        await _repository.AddUserAsync(user, CancellationTokenSource.Token);

        user.Password.Should().Be(hashedPassword);
    }

    [Fact]
    public async Task TheSameUser_WhenUserExists()
    {
        await FillDbContext();
        var expectedUsers = from user in Context.Users.Where(u => u.Id == DefaultUserIdInDbContext).Include(u => u.Province)
                            join country in Context.Countries on user.Province.CountryId equals country.Id
                            select new UserResponse(user.Id, user.Login, user.Province.Name, country.Name);
        var expectedUser = await expectedUsers.SingleAsync();

        var actualUser = await _repository.GetUserByIdAsync(DefaultUserIdInDbContext, CancellationTokenSource.Token);

        actualUser.Should().BeEquivalentTo(expectedUser);
    }

    [Fact]
    public async Task Null_WhenUserDoesNotExist()
    {
        var user = await _repository.GetUserByIdAsync(999, CancellationTokenSource.Token);

        user.Should().BeNull();
    }

    [Fact]
    public async Task Empty_WhenNoUsersExist()
    {
        var users = await _repository.GetAllUsersAsync(CancellationTokenSource.Token);

        users.Should().BeEmpty();
    }

    [Fact]
    public async Task AllUsers_WhenUsersExist()
    {
        await FillDbContext();
        var expectedUsers = from user in Context.Users.Include(u => u.Province)
                            join country in Context.Countries on user.Province.CountryId equals country.Id
                            select new UserResponse(user.Id, user.Login, user.Province.Name, country.Name);

        var actualUsers = await _repository.GetAllUsersAsync(CancellationTokenSource.Token);

        actualUsers.Should().BeEquivalentTo(expectedUsers);
    }
}