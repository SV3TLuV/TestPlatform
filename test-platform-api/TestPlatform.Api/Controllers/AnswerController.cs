using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPlatform.Application.Features.Answers.Commands.Create;
using TestPlatform.Application.Features.Answers.Commands.Delete;
using TestPlatform.Application.Features.Answers.Commands.Update;
using TestPlatform.Application.Features.Answers.Queries.Get;
using TestPlatform.Application.Features.Answers.Queries.GetList;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Api.Controllers;

public class AnswerController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AnswerViewModel>> Get(int id)
    {
        var query = new GetAnswerQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<AnswerViewModel>>> GetAll(
        [FromQuery] GetAnswerListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CreateAnswerCommand command)
    {
        var answerId = await Mediator.Send(command);
        return Created(string.Empty, answerId);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Put([FromBody] UpdateAnswerCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteAnswerCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}