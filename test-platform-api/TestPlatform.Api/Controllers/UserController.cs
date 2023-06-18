using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPlatform.Application.Features.Users.Commands.Create;
using TestPlatform.Application.Features.Users.Commands.Delete;
using TestPlatform.Application.Features.Users.Commands.Update;
using TestPlatform.Application.Features.Users.Queries.Get;
using TestPlatform.Application.Features.Users.Queries.GetList;
using TestPlatform.Application.Features.Users.Queries.GetUserTestsList;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Api.Controllers;

public class UserController : BaseController
{
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<UserViewModel>> Get(int id)
    {
        var query = new GetUserQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ICollection<UserViewModel>>> GetAll(
        [FromQuery] GetUserListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id:int}/Tests")]
    [Authorize]
    public async Task<ActionResult<ICollection<TestViewModel>>> GetUserTests(int id)
    {
        var query = new GetUserTestsListQuery(id);
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
    {
        var userId = await Mediator.Send(command);
        return Created(string.Empty, userId);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Put([FromBody] UpdateUserCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}