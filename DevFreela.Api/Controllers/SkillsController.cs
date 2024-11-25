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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllSkillsQuery();
        var skills = await _mediator.Send(query);

        return Ok(skills);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] InsertSkillCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(Get), new { id = result.Data }, command);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var command = new DeleteSkillCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return NoContent();
    }
}