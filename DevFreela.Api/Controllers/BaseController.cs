using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
}