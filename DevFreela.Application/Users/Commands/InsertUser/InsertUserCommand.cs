using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Users.Commands.InsertUser;

public record InsertUserCommand : ICommand<Result<long>>
{
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; }
    [Required] public DateOnly BirthDate { get; set; }
    public HashSet<long>? Skills { get; set; } = [];
}