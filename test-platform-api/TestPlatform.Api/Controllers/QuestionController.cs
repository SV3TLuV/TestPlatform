using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPlatform.Application.Features.Questions.Commands.Create;
using TestPlatform.Application.Features.Questions.Commands.Delete;
using TestPlatform.Application.Features.Questions.Commands.Update;
using TestPlatform.Application.Features.Questions.Queries.Get;
using TestPlatform.Application.Features.Questions.Queries.GetList;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Api.Controllers;

public class QuestionController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuestionViewModel>> Get(int id)
    {
        var query = new GetQuestionQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<QuestionViewModel>>> GetAll(
        [FromQuery] GetQuestionListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CreateQuestionCommand command)
    {
        var questionId = await Mediator.Send(command);
        return Created(string.Empty, questionId);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Put([FromBody] UpdateQuestionCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteQuestionCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}