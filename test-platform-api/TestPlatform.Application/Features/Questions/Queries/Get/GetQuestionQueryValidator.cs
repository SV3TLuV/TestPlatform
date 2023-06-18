using FluentValidation;

namespace TestPlatform.Application.Features.Questions.Queries.Get;

public class GetQuestionQueryValidator : AbstractValidator<GetQuestionQuery>
{
    public GetQuestionQueryValidator()
    {
        RuleFor(query => query.Id)
            .GreaterThan(0);
    }
}