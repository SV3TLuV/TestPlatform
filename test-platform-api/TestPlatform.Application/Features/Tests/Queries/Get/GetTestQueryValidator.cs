using FluentValidation;

namespace TestPlatform.Application.Features.Tests.Queries.Get;

public class GetTestQueryValidator : AbstractValidator<GetTestQuery>
{
    public GetTestQueryValidator()
    {
        RuleFor(query => query.Id)
            .GreaterThan(0);
    }
}