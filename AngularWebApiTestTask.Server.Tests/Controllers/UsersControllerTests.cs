using AngularWebApiTestTask.Server.Contracts;
using AngularWebApiTestTask.Server.Controllers;
using AngularWebApiTestTask.Server.Domain;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AngularWebApiTestTask.Server.Tests.Controllers;

public class UsersControllerTests: ControllerTestsBase
{
    private readonly UsersController _controller;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UsersControllerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _controller = new UsersController(_userRepositoryMock.Object);
    }

    private static UserResponse CreateUserResponse(int id = 0)
    {
        return new UserResponseBuilder { Id = id }.Build();
    }

    private static CreateUserResponse CreateAnyCreateUserResponse()
    {
        return new CreateUserResponse("Login", "Error");
    }

    [Fact]
    public async Task CreatedResult_WhenUserIsRegistered()
    {
        var user = new UserBuilder{Id = 5}.Build();
        var expectedCreateUserResponse = CreateAnyCreateUserResponse();
        _userRepositoryMock.Setup(repository => repository.AddUserAsync(user, CancellationTokenSource.Token))
            .ReturnsAsync(expectedCreateUserResponse);

        var result = await _controller.RegisterUser(user, CancellationTokenSource.Token);

        var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Which;
        createdResult.Value.Should().Be(expectedCreateUserResponse);
        createdResult.RouteValues.Should().NotBeNull()
            .And.ContainKey("id").WhoseValue.Should().Be(user.Id);
    }

    [Fact]
    public async Task OkResult_WhenUserExists()
    {
        const int userId = 1;
        var expectedUser = CreateUserResponse(userId);

        _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(userId, CancellationTokenSource.Token))
            .ReturnsAsync(expectedUser);

        var result = await _controller.GetUser(userId, CancellationTokenSource.Token);

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be(expectedUser);
    }

    [Fact]
    public async Task NotFound_WhenUserDoesNotExist()
    {
        _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(It.IsAny<int>(), CancellationTokenSource.Token))
            .ReturnsAsync((UserResponse?)null);

        var result = await _controller.GetUser(1, CancellationTokenSource.Token);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task OkResult_WithListOfUsers()
    {
        var expectedUsers = new[]
        {
            CreateUserResponse(), CreateUserResponse(), CreateUserResponse()
        };
        _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync(CancellationTokenSource.Token))
            .ReturnsAsync(expectedUsers);

        var result = await _controller.GetUsers(CancellationTokenSource.Token);

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<UserResponse[]>()
            .Which.Should().Equal(expectedUsers, ReferenceEquals);
    }
}