using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Infrastructure;
//using AngularWebApiTestTask.Server.Mics;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AngularWebApiTestTask.Server.Tests.Infrastructure;

public class UserRepositoryTests : RepositoryBaseTests
{
    private readonly UserRepository _repository;
    private Mock<IPasswordHasher<User>> _passwordHasherMock;

    public UserRepositoryTests()
    {
        _passwordHasherMock = new Mock<IPasswordHasher<User>>();
        _passwordHasherMock.Setup(hasher => hasher.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
            .Returns<User, string>((_, password) => password);

        _repository = new UserRepository(Context, _passwordHasherMock.Object);
    }

    [Fact]
    public async Task AddsUser_WhenUserIsValid()
    {
        var expectedUser = UserBuilder.Any();

        var actualUser = await _repository.AddUserAsync(expectedUser);

        actualUser.Should().Be(expectedUser);
        Context.Users.SingleOrDefault(user => user.Equals(expectedUser))
            .Should().NotBeNull();
    }

    [Fact]
    public async Task AddsUser_WithHashedPassword()
    {
        const string inputtedPassword = "inputted_password";
        var user = new UserBuilder{ Password = inputtedPassword }.Build();
        const string hashedPassword = "hashed_password";
        _passwordHasherMock.Setup(hasher => hasher.HashPassword(user, inputtedPassword))
            .Returns<User, string>((_,_) => hashedPassword);

        var actualUser = await _repository.AddUserAsync(user);

        actualUser.Should().BeEquivalentTo(new UserBuilder { Password = hashedPassword }.Build());
    }

    [Fact]
    public async Task TheSameUser_WhenUserExists()
    {
        const int userId = 100500;
        var expectedUser = new UserBuilder { Id = userId }.Build();
        await Context.Users.AddAsync(expectedUser);
        await Context.SaveChangesAsync();

        var actualUser = await _repository.GetUserByIdAsync(userId);

        actualUser.Should().BeSameAs(expectedUser);
    }

    [Fact]
    public async Task Null_WhenUserDoesNotExist()
    {
        var user = await _repository.GetUserByIdAsync(999);

        user.Should().BeNull();
    }

    [Fact]
    public async Task Empty_WhenNoUsersExist()
    {
        var users = await _repository.GetAllUsersAsync();

        users.Should().BeEmpty();
    }

    [Fact]
    public async Task AllUsers_WhenUsersExist()
    {
        var expectedUsers = new List<User>
        {
            UserBuilder.Any(),
            UserBuilder.Any(),
            UserBuilder.Any(),
        };
        await Context.Users.AddRangeAsync(expectedUsers);
        await Context.SaveChangesAsync();

        var actualUsers = await _repository.GetAllUsersAsync();

        actualUsers.Should().Equal(expectedUsers, ReferenceEquals);
    }
}