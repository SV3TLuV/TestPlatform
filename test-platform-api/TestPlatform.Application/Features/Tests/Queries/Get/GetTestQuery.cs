using MediatR;
using TestPlatform.Application.ViewModels;

namespace TestPlatform.Application.Features.Tests.Queries.Get;

public sealed record GetTestQuery(int Id) : IRequest<TestViewModel>;