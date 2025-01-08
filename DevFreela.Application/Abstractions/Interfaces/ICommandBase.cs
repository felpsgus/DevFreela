using MediatR;

namespace DevFreela.Application.Abstractions.Interfaces;

public interface ICommand : IRequest, ICommandBase
{
}

public interface ICommand<T> : IRequest<T>, ICommandBase
{
}

public interface ICommandBase
{
}