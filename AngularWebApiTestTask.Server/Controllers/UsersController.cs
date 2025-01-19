using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiTestTask.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="user">The user to register</param>
    /// <returns>The registered user</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> RegisterUser(User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdUser = await userRepository.AddUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    /// <summary>
    /// Gets a user by their id
    /// </summary>
    /// <param name="id">The id of the user to retrieve</param>
    /// <returns>The requested user</returns>
    /// <returns>The user</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    /// <summary>
    /// Gets all registered users
    /// </summary>
    /// <returns>A list of users</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto[]>> GetUsers()
    {
        var users = await userRepository.GetAllUsersAsync();
        return Ok(users);
    }
}
