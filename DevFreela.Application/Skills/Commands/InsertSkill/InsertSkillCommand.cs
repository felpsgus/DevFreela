using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Skills.Commands.InsertSkill;

public record InsertSkillCommand : ICommand<Result<long>>
{
    [Required] public string Description { get; set; }
}