namespace TestPlatform.Application.Features.Base.Queries;

public abstract record PaginatedQuery(int Page = 1, int PageSize = 20);