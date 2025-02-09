using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Commands.InsertUser;

public sealed class InsertUserHandler : IRequestHandler<InsertUserCommand, Result<long>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public InsertUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAuthService authService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<Result<long>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(
            request.Name,
            request.Email,
            request.BirthDate,
            _authService.ComputeHash(request.Password),
            request.Roles);
        await _userRepository.AddAsync(user, cancellationToken);
        user.UpdateSkills(request.Skills?.ToList() ?? []);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}