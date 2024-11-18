using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Users.Commands.InsertUser;

public class InsertUserCommand : IRequest<ResultViewModel<long>>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}