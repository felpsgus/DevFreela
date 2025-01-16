using DevFreela.Application.Abstractions;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Skills.Commands.DeleteSkill;

public sealed class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, Result>
{
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSkillHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
    {
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetByIdAsync(request.Id, cancellationToken);
        _skillRepository.Delete(skill);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}