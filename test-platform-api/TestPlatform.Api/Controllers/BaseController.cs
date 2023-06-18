using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TestPlatform.Api.Controllers;

[ApiController]
[EnableCors("CORS")]
[Route("/api/[controller]")]
public class BaseController : ControllerBase
{
    public IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}