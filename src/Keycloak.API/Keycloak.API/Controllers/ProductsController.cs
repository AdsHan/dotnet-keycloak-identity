using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycloak.API.Controllers;

[Authorize()]
[Produces("application/json")]
[Route("api/auth")]
[ApiController]

public class ProductsController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}
