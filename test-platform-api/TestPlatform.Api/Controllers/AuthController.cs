using Microsoft.AspNetCore.Mvc;
using TestPlatform.Application.Features.Auth.Commands.Login;
using TestPlatform.Application.Features.Auth.Commands.Refresh;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Api.Controllers;

public class AuthController : BaseController
{
    [HttpPost]
    [Route("Login", Name = "Login")]
    public async Task<ActionResult<AuthResponseViewModel>> Login(
        [FromBody] LoginCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [Route("Refresh", Name = "Refresh")]
    public async Task<ActionResult<AuthResponseViewModel>> Refresh(
        [FromBody] RefreshCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}