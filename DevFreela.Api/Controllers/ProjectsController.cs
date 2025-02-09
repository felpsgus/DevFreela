using DevFreela.Application.Abstractions;
using DevFreela.Application.Projects.Commands.AddComment;
using DevFreela.Application.Projects.Commands.CompleteProject;
using DevFreela.Application.Projects.Commands.DeleteProject;
using DevFreela.Application.Projects.Commands.InsertProject;
using DevFreela.Application.Projects.Commands.StartProject;
using DevFreela.Application.Projects.Commands.UpdateProject;
using DevFreela.Application.Projects.Queries.GetAllProjects;
using DevFreela.Application.Projects.Queries.GetProjectById;
using DevFreela.Application.Views;
using DevFreela.Domain.Enums;
using DevFreela.Infra.Auth;
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
    [ProducesResponseType(typeof(Result<List<ProjectItemViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetAllProjectsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves details of a specific project by its ID.
    /// </summary>
    /// <param name="id">The ID of the project.</param>
    /// <returns>The project details.</returns>
    /// <response code="200">Returns the project details.</response>
    /// <response code="404">If the project is not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Result<ProjectViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ProjectViewModel>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(id), cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    /// <summary>
    /// Creates a new project with the provided details.
    /// </summary>
    /// <param name="command">The details of the project to create.</param>
    /// <returns>The created project.</returns>
    /// <response code="201">Returns the created project.</response>
    /// <response code="400">If the project details are invalid.</response>
    [HttpPost]
    [HasPermission(RoleEnum.Client)]
    [ProducesResponseType(typeof(Result<long>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<long>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] InsertProjectCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

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
    [HasPermission(RoleEnum.Client)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return NoContent();
    }

    /// <summary>
    /// Deletes a project identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the project is deleted successfully.</response>
    /// <response code="404">If the project is not found.</response>
    [HttpDelete("{id:long}")]
    [HasPermission(RoleEnum.Client)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(id), cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result);

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
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostComment(long id, [FromBody] AddCommentCommand command,
        CancellationToken cancellationToken)
    {
        command.ProjectId = id;
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return NoContent();
    }

    /// <summary>
    /// Starts a project identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to start.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the project is started successfully.</response>
    /// <response code="404">If the project is not found.</response>
    [HttpPut("{id:long}/start")]
    [HasPermission(RoleEnum.Client)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Start(long id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new StartProjectCommand(id), cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }

    /// <summary>
    /// Completes a project identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to complete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the project is completed successfully.</response>
    /// <response code="404">If the project is not found.</response>
    [HttpPut("{id:long}/complete")]
    [HasPermission(RoleEnum.Client)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Complete(long id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CompleteProjectCommand(id), cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }
}