using AngularWebApiTestTask.Server.Controllers;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Infrastructure;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AngularWebApiTestTask.Server.Tests.Controllers;

public class UsersControllerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _controller = new UsersController(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task CreatedResult_WhenUserIsRegistered()
    {
        var expectedUser = new UserBuilder{Id = 5}.Build();
        _userRepositoryMock.Setup(repository => repository.AddUserAsync(expectedUser))
            .ReturnsAsync(expectedUser);

        var result = await _controller.RegisterUser(expectedUser);

        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Which;
        createdResult.Value.Should().Be(expectedUser);
        createdResult.RouteValues.Should().NotBeNull()
            .And.ContainKey("id").WhoseValue.Should().Be(expectedUser.Id);
    }

    [Fact]
    public async Task OkResult_WhenUserExists()
    {
        const int userId = 1;
        var user = new UserBuilder { Id = userId }.Build();

        _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        var result = await _controller.GetUser(userId);

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be(user);
    }

    [Fact]
    public async Task NotFound_WhenUserDoesNotExist()
    {
        _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((User?)null);

        var result = await _controller.GetUser(1);

        result.Result.Should().BeOfType<NotFoundResult>();
    }
  
    [Fact]
    public async Task OkResult_WithListOfUsers()
    {
        var expectedUsers = new List<User>
        {
            UserBuilder.Any(), UserBuilder.Any(), UserBuilder.Any()
        };
        _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync())
            .ReturnsAsync(expectedUsers);

        var result = await _controller.GetUsers();

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IEnumerable<User>>()
            .Which.Should().Equal(expectedUsers, ReferenceEquals);
    }
}