using DevFreela.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.Infra.Auth;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }

    public HasPermissionAttribute(params RoleEnum[] roles)
    {
        Roles = string.Join(",", roles.Select(r => r.ToString()));
    }
}