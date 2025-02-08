using DevFreela.Application.Abstractions;
using DevFreela.Application.Skills.Commands.DeleteSkill;
using DevFreela.Application.Skills.Commands.InsertSkill;
using DevFreela.Application.Skills.Queries.GetAllSkills;
using DevFreela.Application.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api.Controllers;

public class SkillsController : BaseController
{
    private readonly IMediator _mediator;

    public SkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves a list of all skills.
    /// </summary>
    /// <returns>A list of skills.</returns>
    /// <response code="200">Returns the list of skills.</response>
    [HttpGet]
    [ProducesResponseType(typeof(Result<List<SkillItemViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetAllSkillsQuery();
        var skills = await _mediator.Send(query, cancellationToken);

        return Ok(skills);
    }

    /// <summary>
    /// Creates a new skill with the provided details.
    /// </summary>
    /// <param name="command">The details of the skill to create.</param>
    /// <returns>The created skill.</returns>
    /// <response code="201">Returns the created skill.</response>
    /// <response code="400">If the skill details are invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<long>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] InsertSkillCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(Get), new { id = result.Data }, command);
    }

    /// <summary>
    /// Deletes a skill identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the skill to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the skill is deleted successfully.</response>
    /// <response code="404">If the skill is not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var command = new DeleteSkillCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }
}