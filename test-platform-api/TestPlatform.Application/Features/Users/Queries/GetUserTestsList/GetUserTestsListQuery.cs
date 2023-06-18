using MediatR;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Application.Features.Users.Queries.GetUserTestsList;

public sealed record GetUserTestsListQuery(int Id) : IRequest<ICollection<TestViewModel>>;