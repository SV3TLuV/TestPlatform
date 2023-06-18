using AutoMapper;
using MediatR;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Questions.Commands.Create;

public sealed class CreateQuestionCommand : IRequest<int>, IMapWith<Question>
{
    public string Text { get; set; } = null!;
    public int TestId { get; set; }
}