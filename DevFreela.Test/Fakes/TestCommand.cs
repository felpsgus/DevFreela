using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Test.Fakes;

public class TestCommand : ICommand<Result>
{
    public string Name { get; set; }
}