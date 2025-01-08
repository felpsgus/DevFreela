using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public abstract class BaseController : ControllerBase
{
}