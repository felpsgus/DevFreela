using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<ResultViewModel<List<GetAllUsersViewModel>>>
{
}