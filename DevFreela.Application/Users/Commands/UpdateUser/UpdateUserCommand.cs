using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : ICommand<Result>
{
    public long Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; }
    public HashSet<long>? Skills { get; set; } = [];
}