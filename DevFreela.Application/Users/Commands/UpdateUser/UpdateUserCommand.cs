using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<ResultViewModel>
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}