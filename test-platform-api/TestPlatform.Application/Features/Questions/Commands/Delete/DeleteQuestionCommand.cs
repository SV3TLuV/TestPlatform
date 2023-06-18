using MediatR;

namespace TestPlatform.Application.Features.Questions.Commands.Delete;

public sealed record DeleteQuestionCommand(int Id) : IRequest;