using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<ResultViewModel>
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    public HashSet<long>? Skills { get; set; } = [];
}