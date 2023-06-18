using MediatR;

namespace TestPlatform.Application.Features.Answers.Commands.Delete;

public sealed record DeleteAnswerCommand(int Id) : IRequest;