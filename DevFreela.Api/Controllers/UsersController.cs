using DevFreela.Application.Models;
using DevFreela.Application.Users.Commands.DeleteUser;
using DevFreela.Application.Users.Commands.InsertUser;
using DevFreela.Application.Users.Commands.UpdateUser;
using DevFreela.Application.Users.Queries.GetAllUsers;
using DevFreela.Application.Users.Queries.GetUserById;
using MediatR;
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
    [ProducesResponseType(typeof(ResultViewModel<List<UserItemViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query);

        return Ok(users);
    }

    /// <summary>
    /// Retrieves details of a specific user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The user details.</returns>
    /// <response code="200">Returns the user details.</response>
    /// <response code="400">If the user is not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ResultViewModel<UserViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultViewModel<UserViewModel>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(long id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query);

        if (!user.IsSuccess)
            return BadRequest(user.Message);

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
    [ProducesResponseType(typeof(ResultViewModel<long>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResultViewModel<long>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] InsertUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(Get), new { id = result.Data }, command);
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
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return NoContent();
    }

    /// <summary>
    /// Deletes a user identified by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the user is deleted successfully.</response>
    /// <response code="400">If the user is not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(long id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return NoContent();
    }
}