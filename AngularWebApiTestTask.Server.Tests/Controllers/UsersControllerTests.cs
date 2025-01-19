using AngularWebApiTestTask.Server.Controllers;
using AngularWebApiTestTask.Server.Domain;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AngularWebApiTestTask.Server.Tests.Controllers;

public class UsersControllerTests
{
    private readonly UsersController _controller;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UsersControllerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _controller = new UsersController(_userRepositoryMock.Object);
    }

    private static UserDto CreateUserDto(int id = 0)
    {
        return new UserDtoBuilder { Id = id }.Build();
    }

    [Fact]
    public async Task CreatedResult_WhenUserIsRegistered()
    {
        var user = UserBuilder.Any();
        var expectedUser = CreateUserDto(5);
        _userRepositoryMock.Setup(repository => repository.AddUserAsync(user))
            .ReturnsAsync(expectedUser);

        var result = await _controller.RegisterUser(user);

        var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Which;
        createdResult.Value.Should().Be(expectedUser);
        createdResult.RouteValues.Should().NotBeNull()
            .And.ContainKey("id").WhoseValue.Should().Be(expectedUser.Id);
    }

    [Fact]
    public async Task OkResult_WhenUserExists()
    {
        const int userId = 1;
        var expectedUser = CreateUserDto(userId);

        _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(userId))
            .ReturnsAsync(expectedUser);

        var result = await _controller.GetUser(userId);

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be(expectedUser);
    }

    [Fact]
    public async Task NotFound_WhenUserDoesNotExist()
    {
        _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((UserDto?)null);

        var result = await _controller.GetUser(1);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task OkResult_WithListOfUsers()
    {
        var expectedUsers = new[]
        {
            CreateUserDto(), CreateUserDto(), CreateUserDto()
        };
        _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync())
            .ReturnsAsync(expectedUsers);

        var result = await _controller.GetUsers();

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<UserDto[]>()
            .Which.Should().Equal(expectedUsers, ReferenceEquals);
    }
}