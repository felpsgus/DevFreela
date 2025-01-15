using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(long Id) : IRequest<Result<UserViewModel>>;