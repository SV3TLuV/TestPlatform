using MediatR;

namespace TestPlatform.Application.Features.Tests.Commands.Delete;

public sealed record DeleteTestCommand(int Id) : IRequest;