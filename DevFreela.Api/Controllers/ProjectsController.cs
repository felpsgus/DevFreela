using DevFreela.Application.Projects.Commands.CompleteProject;
using DevFreela.Application.Projects.Commands.DeleteProject;
using DevFreela.Application.Projects.Commands.InsertComment;
using DevFreela.Application.Projects.Commands.InsertProject;
using DevFreela.Application.Projects.Commands.StartProject;
using DevFreela.Application.Projects.Commands.UpdateProject;
using DevFreela.Application.Projects.Queries.GetAllProjects;
using DevFreela.Application.Projects.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api.Controllers;

public class ProjectsController : BaseController
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllProjectsQuery();
        var projects = await _mediator.Send(query);

        return Ok(projects);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery(id));

        if (project == null)
        {
            return BadRequest();
        }

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] InsertProjectCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(Get), new { id = result.Data }, command);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateProjectCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPost("{id:long}/comments")]
    public async Task<IActionResult> PostComment(long id, [FromBody] InsertCommentCommand command)
    {
        command.IdProject = id;
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPut("{id:long}/start")]
    public async Task<IActionResult> Start(long id)
    {
        var result = await _mediator.Send(new StartProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPut("{id:long}/complete")]
    public async Task<IActionResult> Complete(long id)
    {
        var result = await _mediator.Send(new CompleteProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }
}