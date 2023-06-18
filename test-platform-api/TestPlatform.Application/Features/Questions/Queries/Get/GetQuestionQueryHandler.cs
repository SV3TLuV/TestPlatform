using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Exceptions;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Questions.Queries.Get;

public class GetQuestionQueryHandler : IRequestHandler<GetQuestionQuery, QuestionViewModel>
{
    private readonly ITestDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionQueryHandler(ITestDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QuestionViewModel> Handle(GetQuestionQuery request,
        CancellationToken cancellationToken)
    {
        var question = await _context.Set<Question>()
            .AsNoTrackingWithIdentityResolution()
            .Include(question => question.Answers)
            .FirstOrDefaultAsync(e => e.QuestionId == request.Id, cancellationToken);

        if (question is null)
            throw new NotFoundException(nameof(Question), request.Id);

        return _mapper.Map<QuestionViewModel>(question);
    }
}