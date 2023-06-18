using MediatR;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Application.Features.Users.Queries.Get;

public sealed record GetUserQuery(int Id) : IRequest<UserViewModel>;