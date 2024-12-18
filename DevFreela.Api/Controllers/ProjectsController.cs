using DevFreela.Application.Models;
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

    /// <summary>
    /// Retrieves a list of all projects.
    /// </summary>
    /// <returns>A list of projects.</returns>
    /// <response code="200">Returns the list of projects.</response>
    [HttpGet]
    [ProducesResponseType(typeof(ResultViewModel<List<ProjectItemViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllProjectsQuery();
        var projects = await _mediator.Send(query);

        return Ok(projects);
    }

    /// <summary>
    /// Retrieves details of a specific project by its ID.
    /// </summary>
    /// <param name="id">The ID of the project.</param>
    /// <returns>The project details.</returns>
    /// <response code="200">Returns the project details.</response>
    /// <response code="400">If the project is not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ResultViewModel<ProjectViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultViewModel<ProjectViewModel>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(long id)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery(id));

        if (project == null)
        {
            return BadRequest();
        }

        return Ok(project);
    }

    /// <summary>
    /// Creates a new project with the provided details.
    /// </summary>
    /// <param name="command">The details of the project to create.</param>
    /// <returns>The created project.</returns>
    /// <response code="201">Returns the created project.</response>
    /// <response code="400">If the project details are invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ResultViewModel<long>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResultViewModel<long>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] InsertProjectCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(Get), new { id = result.Data }, command);
    }

    /// <summary>
    /// Updates the details of an existing project identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to update.</param>
    /// <param name="command">The updated project details.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the project is updated successfully.</response>
    /// <response code="400">If the project details are invalid.</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Deletes a project identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the project is deleted successfully.</response>
    /// <response code="400">If the project is not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    /// <summary>
    /// Adds a comment to a project identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the project.</param>
    /// <param name="command">The comment details.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the comment is added successfully.</response>
    /// <response code="400">If the comment details are invalid.</response>
    [HttpPost("{id:long}/comments")]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Starts a project identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to start.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the project is started successfully.</response>
    /// <response code="400">If the project is not found.</response>
    [HttpPut("{id:long}/start")]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Start(long id)
    {
        var result = await _mediator.Send(new StartProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    /// <summary>
    /// Completes a project identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to complete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the project is completed successfully.</response>
    /// <response code="400">If the project is not found.</response>
    [HttpPut("{id:long}/complete")]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
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