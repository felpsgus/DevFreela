using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : ICommand<Result>
{
    [JsonIgnore] public long Id { get; set; }
    [Required] public string Name { get; init; }
    [Required] public string Email { get; init; }
    public HashSet<long>? Skills { get; init; } = [];
}