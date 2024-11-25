using DevFreela.Application.Models;
using DevFreela.Application.Skills.Commands.DeleteSkill;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Skills.Commands.DeleteSkill;

public class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, ResultViewModel>
{
    private readonly ISkillRepository _skillRepository;

    public DeleteSkillHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<ResultViewModel> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var skill = await _skillRepository.GetByIdAsync(request.Id);
            if (skill == null)
            {
                return ResultViewModel.Error("Skill does not exist.");
            }

            await _skillRepository.DeleteAsync(skill);
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error("An error occurred while deleting the skill.");
            throw;
        }
    }
}