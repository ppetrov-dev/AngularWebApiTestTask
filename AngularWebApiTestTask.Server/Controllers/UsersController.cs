using AngularWebApiTestTask.Server.Contracts;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiTestTask.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="user">The user to register</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The registered user</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateUserResponse>> RegisterUser(User user, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createUserResponse = await userRepository.AddUserAsync(user, cancellationToken);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createUserResponse);
    }

    /// <summary>
    /// Gets a user by their id
    /// </summary>
    /// <param name="id">The id of the user to retrieve</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested user</returns>
    /// <returns>The user</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> GetUser(int id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(id, cancellationToken);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    /// <summary>
    /// Gets all registered users
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of users</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse[]>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllUsersAsync(cancellationToken);
        return Ok(users);
    }
}
