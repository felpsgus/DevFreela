using DevFreela.Domain.Entities;

namespace DevFreela.Application.Abstractions.Interfaces;

public interface IAuthService
{
    string ComputeHash(string password);

    string GenerateJwtToken(User user);
}