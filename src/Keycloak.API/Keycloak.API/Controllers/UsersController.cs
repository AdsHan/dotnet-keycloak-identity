using Keycloak.API.Application.InputModels;
using Keycloak.API.Application.Messages.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycloak.API.Controllers;

[Produces("application/json")]
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("create-user")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ActionName("NewUser")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserInputModel input, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new CreateUserCommand(input.FirstName, input.LastName, input.Email, input.Password), cancellationToken);

        return result.IsValid() ? CreatedAtAction("NewUser", new { id = result.Response }, input) : BadRequest(result.Errors);
    }

}
