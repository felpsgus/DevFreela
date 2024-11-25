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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query);

        return Ok(users);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query);

        if (!user.IsSuccess)
            return BadRequest(user.Message);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] InsertUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(Get), new { id = result.Data }, command);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return NoContent();
    }
}