using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Skills.Commands.DeleteSkill;

public record DeleteSkillCommand(long Id) : ICommand<Result>;