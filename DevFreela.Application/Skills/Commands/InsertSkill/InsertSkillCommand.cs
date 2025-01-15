using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Skills.Commands.InsertSkill;

public record InsertSkillCommand(string Description) : ICommand<Result<long>>;