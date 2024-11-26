using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Users.Commands.InsertUser;

public class InsertUserCommand : IRequest<ResultViewModel<long>>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public DateOnly BirthDate { get; set; }
    public HashSet<long>? Skills { get; set; } = [];
}