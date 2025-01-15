using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using MediatR;

namespace DevFreela.Application.Skills.Queries.GetAllSkills;

public record GetAllSkillsQuery : IRequest<Result<List<SkillItemViewModel>>>;