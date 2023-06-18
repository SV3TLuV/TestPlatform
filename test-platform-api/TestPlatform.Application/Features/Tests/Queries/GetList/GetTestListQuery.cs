using MediatR;
using TestPlatform.Application.Features.Base.Queries;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Queries.GetList;

public sealed record GetTestListQuery
    : PaginatedQuery, IRequest<PagedList<TestViewModel>>
{
    public string? SearchedText { get; init; } = null!;
}