using DevFreela.Application.Abstractions;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Commands.InsertUser;

public sealed class InsertUserHandler : IRequestHandler<InsertUserCommand, Result<long>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InsertUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<long>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email, request.BirthDate);
        await _userRepository.AddAsync(user, cancellationToken);
        user.UpdateSkills(request.Skills?.ToList() ?? []);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}