using DevFreela.Application.Models;
using DevFreela.Application.Skills.Commands.DeleteSkill;
using DevFreela.Application.Skills.Commands.InsertSkill;
using DevFreela.Application.Skills.Queries.GetAllSkills;
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
    [ProducesResponseType(typeof(ResultViewModel<List<SkillItemViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllSkillsQuery();
        var skills = await _mediator.Send(query);

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
    [ProducesResponseType(typeof(ResultViewModel<long>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResultViewModel<long>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] InsertSkillCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(Get), new { id = result.Data }, command);
    }

    /// <summary>
    /// Deletes a skill identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the skill to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the skill is deleted successfully.</response>
    /// <response code="400">If the skill is not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(long id)
    {
        var command = new DeleteSkillCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return NoContent();
    }
}