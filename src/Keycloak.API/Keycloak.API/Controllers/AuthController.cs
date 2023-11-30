using Keycloak.API.Application.InputModels;
using Keycloak.API.Application.Messages.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycloak.API.Controllers;

[Produces("application/json")]
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignInAsync([FromBody] SignInInputModel input)
    {
        var result = await _sender.Send(new SignInCommand(input.UserName, input.Password));

        if (!result.IsValid())
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result.Response);
    }
}

