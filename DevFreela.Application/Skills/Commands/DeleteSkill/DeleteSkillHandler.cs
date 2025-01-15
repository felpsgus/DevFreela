using DevFreela.Application.Abstractions;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Skills.Commands.DeleteSkill;

public sealed class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, Result>
{
    private readonly ISkillRepository _skillRepository;

    public DeleteSkillHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<Result> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetByIdAsync(request.Id, cancellationToken);

        await _skillRepository.DeleteAsync(skill, cancellationToken);
        return Result.Success();
    }
}