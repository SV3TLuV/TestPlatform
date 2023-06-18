using AutoMapper;
using MediatR;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Answers.Commands.Create;

public sealed class CreateAnswerCommand : IRequest<int>, IMapWith<Answer>
{
    public string Text { get; set; } = null!;

    public bool IsRight { get; set; }
    
    public int QuestionId { get; set; }

    public int TestId { get; set; }
}