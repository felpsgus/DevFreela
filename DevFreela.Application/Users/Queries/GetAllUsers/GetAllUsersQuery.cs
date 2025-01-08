using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<Result<List<UserItemViewModel>>>
{
}