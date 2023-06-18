using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPlatform.Application.Features.Tests.Commands.CheckTest;
using TestPlatform.Application.Features.Tests.Commands.Create;
using TestPlatform.Application.Features.Tests.Commands.Delete;
using TestPlatform.Application.Features.Tests.Commands.Update;
using TestPlatform.Application.Features.Tests.Queries.Get;
using TestPlatform.Application.Features.Tests.Queries.GetList;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Api.Controllers;

public class TestController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TestViewModel>> Get(int id)
    {
        var query = new GetTestQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<TestViewModel>>> GetAll(
        [FromQuery] GetTestListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Route("Check", Name = "CheckTest")]
    public async Task<ActionResult<TestResultViewModel>> CheckTest(
        [FromBody] CheckTestCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CreateTestCommand command)
    {
        var testId = await Mediator.Send(command);
        return Created(string.Empty, testId);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Put([FromBody] UpdateTestCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteTestCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}