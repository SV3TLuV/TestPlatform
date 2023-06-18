using MediatR;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Application.Features.Users.Queries.GetList;

public sealed record GetUserListQuery : IRequest<ICollection<UserViewModel>>;