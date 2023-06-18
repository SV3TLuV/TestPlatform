using FluentValidation;

namespace TestPlatform.Application.Features.Users.Queries.Get;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(query => query.Id)
            .GreaterThan(0);
    }
}