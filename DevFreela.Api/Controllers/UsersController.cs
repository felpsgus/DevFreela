using DevFreela.Application.Abstractions;
using DevFreela.Application.Users.Commands.DeleteUser;
using DevFreela.Application.Users.Commands.InsertUser;
using DevFreela.Application.Users.Commands.Login;
using DevFreela.Application.Users.Commands.UpdateUser;
using DevFreela.Application.Users.Queries.GetAllUsers;
using DevFreela.Application.Users.Queries.GetUserById;
using DevFreela.Application.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api.Controllers;

public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves a list of all users.
    /// </summary>
    /// <returns>A list of users.</returns>
    /// <response code="200">Returns the list of users.</response>
    [HttpGet]
    [ProducesResponseType(typeof(Result<List<UserItemViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query, cancellationToken);

        return Ok(users);
    }

    /// <summary>
    /// Retrieves details of a specific user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The user details.</returns>
    /// <response code="200">Returns the user details.</response>
    /// <response code="404">If the user is not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Result<UserViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query, cancellationToken);

        if (!user.IsSuccess)
            return NotFound(user);

        return Ok(user);
    }

    /// <summary>
    /// Creates a new user with the provided details.
    /// </summary>
    /// <param name="command">The details of the user to create.</param>
    /// <returns>The created user.</returns>
    /// <response code="201">Returns the created user.</response>
    /// <response code="400">If the user details are invalid.</response>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Result<long>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] InsertUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Updates the details of an existing user identified by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="command">The updated user details.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the user is updated successfully.</response>
    /// <response code="400">If the user details are invalid.</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return NoContent();
    }

    /// <summary>
    /// Deletes a user identified by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the user is deleted successfully.</response>
    /// <response code="404">If the user is not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }

    /// <summary>
    /// Logs in a user with the provided credentials.
    /// </summary>
    /// <param name="command">The user credentials.</param>
    /// <returns>The login token.</returns>
    /// <response code="200">Returns the login token.</response>
    /// <response code="400">If the user credentials are invalid.</response>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Result<LoginViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}