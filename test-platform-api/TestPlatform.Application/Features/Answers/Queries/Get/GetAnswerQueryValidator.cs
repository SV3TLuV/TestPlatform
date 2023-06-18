using System.Data;
using FluentValidation;
using TestPlatform.Application.Features.Base.Validators;

namespace TestPlatform.Application.Features.Answers.Queries.Get;

public class GetAnswerQueryValidator : AbstractValidator<GetAnswerQuery>
{
    public GetAnswerQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}