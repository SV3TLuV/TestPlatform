using MediatR;

namespace TestPlatform.Application.Features.Users.Commands.Delete;

public sealed record DeleteUserCommand(int Id) : IRequest;